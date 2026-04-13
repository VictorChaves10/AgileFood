using AgileFood.Application.Dtos.Users;
using AgileFood.Application.Interfaces.Users;
using AgileFood.Application.Mappings.Users;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace AgileFood.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<UserResultDto> CreateAsync(CreateUserDto dto)
    {
        var existing = await _unitOfWork.UserRepository.GetByEmailAsync(dto.Email);
        if (existing is not null)
            throw new InvalidOperationException("Já existe um usuário com este e-mail.");

        var user = new User(dto.Name, dto.Email, string.Empty, dto.Role);
        var hash = _passwordHasher.HashPassword(user, dto.Password);
        user.SetPasswordHash(hash);

        _unitOfWork.UserRepository.Add(user);
        await _unitOfWork.CommitAsync();

        return user.MapToUserDto();
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordDto dto)
    {
        var user = await _unitOfWork.UserRepository.FindAsync(u => u.Id == dto.UserId);
        if (user is null) return false;

        var verification = _passwordHasher.VerifyHashedPassword(
            user, user.PasswordHash, dto.CurrentPassword);

        if (verification == PasswordVerificationResult.Failed)
            throw new InvalidOperationException("A senha atual está incorreta.");

        var newHash = _passwordHasher.HashPassword(user, dto.NewPassword);
        user.SetPasswordHash(newHash);

        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _unitOfWork.UserRepository.FindAsync(u => u.Id == id);
        if (user is null) return false;

        _unitOfWork.UserRepository.Remove(user);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<IEnumerable<UserResultDto>> GetAllAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        if (users is null || !users.Any())
            return Enumerable.Empty<UserResultDto>();

        return users.Select(u => u.MapToUserDto());
    }

    public async Task<UserResultDto?> GetByIdAsync(long id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        return user?.MapToUserDto();
    }

    public async Task<bool> UpdateAsync(UpdateUserDto dto)
    {
        var user = await _unitOfWork.UserRepository.FindAsync(u => u.Id == dto.Id);
        if (user is null) return false;

        user.Update(dto.Name, dto.Email, dto.Role, dto.IsActive);
        await _unitOfWork.CommitAsync();
        return true;
    }
}