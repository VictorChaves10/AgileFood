namespace AgileFood.Application.Dtos.Users;

public record ChangePasswordDto(
    long UserId,
    string CurrentPassword,
    string NewPassword
);