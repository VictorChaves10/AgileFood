using System.Security.Cryptography;
using System.Text;
using AgileFood.Application.Dtos.Auth;
using AgileFood.Application.Dtos.Users;
using AgileFood.Application.Interfaces.Auth;
using AgileFood.Application.Interfaces.Notifications;
using AgileFood.Application.Mappings.Users;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace AgileFood.Application.Services.Auth;

public class AuthService : IAuthService
{
    private const int ResetTokenExpirationMinutes = 30;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(IUnitOfWork unitOfWork, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<UserResultDto> LoginAsync(LoginDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);

        if (user is null || !user.IsActive)
            throw new InvalidOperationException("E-mail ou senha inválidos.");

        var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (verification == PasswordVerificationResult.Failed)
            throw new InvalidOperationException("E-mail ou senha inválidos.");

        return user.MapToUserDto();
    }

    public async Task RequestPasswordResetAsync(ForgotPasswordDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);

        if (user is null || !user.IsActive)
            return;

        var token = GenerateResetToken();
        var tokenHash = HashResetToken(token);

        user.SetPasswordResetToken(tokenHash, DateTime.UtcNow.AddMinutes(ResetTokenExpirationMinutes));
        await _unitOfWork.CommitAsync();

        var body =
            $"Olá, {user.Name}.\n\n" +
            $"Use o código abaixo para redefinir sua senha do AgileFood. Ele expira em {ResetTokenExpirationMinutes} minutos.\n\n" +
            $"{token}\n\n" +
            "Se você não solicitou essa redefinição, ignore este e-mail.";

        await _emailSender.SendAsync(user.Email, "Redefinição de senha - AgileFood", body);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);

        if (user is null || !user.HasValidPasswordResetToken(DateTime.UtcNow))
            throw new InvalidOperationException("Token de redefinição inválido ou expirado.");

        var providedTokenHash = HashResetToken(dto.Token);

        if (!FixedTimeEquals(providedTokenHash, user.PasswordResetTokenHash!))
            throw new InvalidOperationException("Token de redefinição inválido ou expirado.");

        if (string.IsNullOrWhiteSpace(dto.NewPassword) || dto.NewPassword.Length < 6)
            throw new InvalidOperationException("A senha deve ter no mínimo 6 caracteres.");

        var newHash = _passwordHasher.HashPassword(user, dto.NewPassword);
        user.CompletePasswordChange(newHash);

        await _unitOfWork.CommitAsync();
    }

    private static string GenerateResetToken() =>
        Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

    private static string HashResetToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

    private static bool FixedTimeEquals(string a, string b) =>
        CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
}
