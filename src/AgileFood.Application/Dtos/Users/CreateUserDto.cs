using AgileFood.Business.Models.Users.Enums;

namespace AgileFood.Application.Dtos.Users;

public record CreateUserDto(
    string Name,
    string Email,
    string Password,
    UserRole Role
);