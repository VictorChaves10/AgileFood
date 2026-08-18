using AgileFood.Business.Models.Users.Enums;

namespace AgileFood.Application.Dtos.Users;

public record UserResultDto(
    long Id,
    string Name,
    string Email,
    string Cpf,
    string? EmployeeCode,
    UserRole Role,
    bool IsActive,
    bool MustChangePassword,
    DateTime CreatedAt
);
