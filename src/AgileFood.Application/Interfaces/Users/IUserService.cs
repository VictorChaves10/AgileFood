using AgileFood.Application.Dtos.Users;

namespace AgileFood.Application.Interfaces.Users;

public interface IUserService
{
    Task<IEnumerable<UserResultDto>> GetAllAsync();
    Task<UserResultDto?> GetByIdAsync(long id);
    Task<UserResultDto> CreateAsync(CreateUserDto dto);
    Task<bool> UpdateAsync(UpdateUserDto dto);
    Task<bool> DeleteAsync(long id);
    Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
}