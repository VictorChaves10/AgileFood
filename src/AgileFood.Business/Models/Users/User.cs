using AgileFood.Business.Models.Users.Enums;

namespace AgileFood.Business.Models.Users;

public class User
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public string Cpf { get; private set; }

    public string PasswordHash { get; private set; }

    public string TransactionPinHash { get; private set; }

    public UserRole Role { get; private set; }

    public bool IsActive { get; private set; }

    public bool MustChangePassword { get; private set; }

    public string? PasswordResetTokenHash { get; private set; }

    public DateTime? PasswordResetTokenExpiresAtUtc { get; private set; }

    public DateTime CreatedAt { get; private set; }

    protected User() { }

    public User(string name, string email, string cpf, string passwordHash, string transactionPinHash, UserRole role)
    {
        ChangeName(name);
        ChangeEmail(email);
        ChangeCpf(cpf);
        SetPasswordHash(passwordHash);
        SetTransactionPinHash(transactionPinHash);
        Role = role;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
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
            throw new ArgumentException("O nome do usuário é obrigatório.", nameof(name));

        Name = name;
    }

    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("O e-mail é obrigatório.", nameof(email));

        Email = email;
    }

    public void ChangeCpf(string cpf)
    {
        var normalizedCpf = NormalizeCpf(cpf);

        if (!IsValidCpf(normalizedCpf))
            throw new ArgumentException("O CPF informado não é válido.", nameof(cpf));

        Cpf = normalizedCpf;
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
