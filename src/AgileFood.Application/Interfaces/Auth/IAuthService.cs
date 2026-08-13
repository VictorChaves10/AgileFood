using AgileFood.Application.Dtos.Auth;
using AgileFood.Application.Dtos.Users;

namespace AgileFood.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<UserResultDto> LoginAsync(LoginDto dto);
}
