namespace AgileFood.Application.Dtos.Users;

public record ChangeTransactionPinDto(
    long UserId,
    string CurrentPin,
    string NewPin
);
