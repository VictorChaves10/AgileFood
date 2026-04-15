namespace AgileFood.Application.Dtos.Consumptions;

public record RegisterConsumptionDto(
    long UserId,
    long ProductId,
    int Quantity
);