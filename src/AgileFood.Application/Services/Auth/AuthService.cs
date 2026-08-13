using AgileFood.Application.Dtos.Auth;
using AgileFood.Application.Dtos.Users;
using AgileFood.Application.Interfaces.Auth;
using AgileFood.Application.Mappings.Users;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace AgileFood.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
}
