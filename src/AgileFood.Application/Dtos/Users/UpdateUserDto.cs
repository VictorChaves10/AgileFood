using AgileFood.Business.Models.Users.Enums;

namespace AgileFood.Application.Dtos.Users;

public record UpdateUserDto(
    long Id,
    string Name,
    string Email,
    UserRole Role,
    bool IsActive
);