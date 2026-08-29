using AgileFood.Business.Exceptions;
using AgileFood.Business.Models.Users.Enums;

namespace AgileFood.Business.Models.Users;

public class User
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Cpf { get; private set; }

    public string? EmployeeCode { get; private set; }

    public string PasswordHash { get; private set; }

    public string TransactionPinHash { get; private set; }

    public UserRole Role { get; private set; }

    public bool IsActive { get; private set; }

    public bool MustChangePassword { get; private set; }

    public string? PasswordResetTokenHash { get; private set; }

    public DateTime? PasswordResetTokenExpiresAtUtc { get; private set; }

    public int FailedPinAttempts { get; private set; }

    public DateTime? PinLockedUntilUtc { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private const int MaxFailedPinAttempts = 5;
    private static readonly TimeSpan PinLockDuration = TimeSpan.FromMinutes(15);

    protected User() { }

    public User(string name, string email, string cpf, string passwordHash, string transactionPinHash, UserRole role, DateTime nowUtc)
    {
        ChangeName(name);
        ChangeEmail(email);
        ChangeCpf(cpf);
        SetPasswordHash(passwordHash);
        SetTransactionPinHash(transactionPinHash);
        Role = role;
        IsActive = true;
        CreatedAt = nowUtc;
    }

    public void Update(string name, string email, string cpf, UserRole role, bool isActive)
    {
        if (Name != name) ChangeName(name);
        if (Email != email) ChangeEmail(email);
        if (Cpf != NormalizeCpf(cpf)) ChangeCpf(cpf);
        if (Role != role) ChangeRole(role);

        if (isActive)
            Activate();
        else
            Deactivate();
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("O nome do usuário é obrigatório.");

        Name = name;
    }

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("O e-mail é obrigatório.");

        Email = email;
    }

    public void ChangeCpf(string cpf)
    {
        var normalizedCpf = NormalizeCpf(cpf);

        if (!IsValidCpf(normalizedCpf))
            throw new DomainException("O CPF informado não é válido.");

        Cpf = normalizedCpf;
    }

    public void SetEmployeeCode(string employeeCode)
    {
        if (string.IsNullOrWhiteSpace(employeeCode))
            throw new ArgumentException("O código do funcionário é obrigatório.", nameof(employeeCode));

        EmployeeCode = employeeCode;
    }

    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("O hash da senha é obrigatório.", nameof(passwordHash));

        PasswordHash = passwordHash;
    }

    public void SetTransactionPinHash(string transactionPinHash)
    {
        if (string.IsNullOrWhiteSpace(transactionPinHash))
            throw new ArgumentException("O hash do PIN é obrigatório.", nameof(transactionPinHash));

        TransactionPinHash = transactionPinHash;
    }

    public void ChangeRole(UserRole role) => Role = role;

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void SetPasswordAsTemporary(string passwordHash)
    {
        SetPasswordHash(passwordHash);
        MustChangePassword = true;
    }

    public void CompletePasswordChange(string passwordHash)
    {
        SetPasswordHash(passwordHash);
        MustChangePassword = false;
        ClearPasswordResetToken();
    }

    public void SetPasswordResetToken(string tokenHash, DateTime expiresAtUtc)
    {
        PasswordResetTokenHash = tokenHash;
        PasswordResetTokenExpiresAtUtc = expiresAtUtc;
    }

    public void ClearPasswordResetToken()
    {
        PasswordResetTokenHash = null;
        PasswordResetTokenExpiresAtUtc = null;
    }

    public bool HasValidPasswordResetToken(DateTime nowUtc) =>
        PasswordResetTokenHash is not null &&
        PasswordResetTokenExpiresAtUtc is not null &&
        PasswordResetTokenExpiresAtUtc.Value > nowUtc;

    public bool IsPinLocked(DateTime nowUtc) =>
        PinLockedUntilUtc is not null && PinLockedUntilUtc.Value > nowUtc;

    public void RegisterFailedPinAttempt(DateTime nowUtc)
    {
        FailedPinAttempts++;

        if (FailedPinAttempts >= MaxFailedPinAttempts)
        {
            PinLockedUntilUtc = nowUtc.Add(PinLockDuration);
            FailedPinAttempts = 0;
        }
    }

    public void ResetPinAttempts()
    {
        FailedPinAttempts = 0;
        PinLockedUntilUtc = null;
    }

    public static string NormalizeCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return string.Empty;

        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    public static bool IsValidCpf(string cpf)
    {
        if (cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

        var firstDigit = CalculateCpfDigit(cpf, 9);
        var secondDigit = CalculateCpfDigit(cpf, 10);

        return cpf[9] - '0' == firstDigit && cpf[10] - '0' == secondDigit;
    }

    private static int CalculateCpfDigit(string cpf, int length)
    {
        var sum = 0;

        for (var i = 0; i < length; i++)
            sum += (cpf[i] - '0') * (length + 1 - i);

        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }
}
