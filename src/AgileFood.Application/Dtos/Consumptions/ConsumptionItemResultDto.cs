namespace AgileFood.Application.Dtos.Consumptions;

public record ConsumptionItemResultDto(
    long Id,
    long ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);
