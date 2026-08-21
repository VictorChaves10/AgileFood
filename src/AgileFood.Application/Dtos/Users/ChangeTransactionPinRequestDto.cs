namespace AgileFood.Application.Dtos.Users;

public record ChangeTransactionPinRequestDto(
    string CurrentPin,
    string NewPin
);
