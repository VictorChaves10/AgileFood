using AgileFood.Business.Models.Users.Enums;

namespace AgileFood.Application.Dtos.Users;

public record UserResultDto(
    long Id,
    string Name,
    string Email,
    string Cpf,
    UserRole Role,
    bool IsActive,
    DateTime CreatedAt
);
