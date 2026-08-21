namespace AgileFood.Application.Dtos.Users;

public record ChangePasswordRequestDto(
    string CurrentPassword,
    string NewPassword
);
