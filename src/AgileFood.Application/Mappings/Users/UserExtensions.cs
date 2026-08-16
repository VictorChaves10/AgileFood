using AgileFood.Application.Dtos.Users;
using AgileFood.Business.Models.Users;

namespace AgileFood.Application.Mappings.Users;

public static class UserExtensions
{
    public static UserResultDto MapToUserDto(this User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserResultDto(
            user.Id,
            user.Name,
            user.Email,
            user.Cpf,
            user.Role,
            user.IsActive,
            user.MustChangePassword,
            user.CreatedAt
        );
    }
}
