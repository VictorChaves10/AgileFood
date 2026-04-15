namespace AgileFood.Application.Dtos.Consumptions;

public record ConsumptionResultDto(
    long Id,
    long UserId,
    string? UserName,
    long ProductId,
    string? ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice,
    DateTime ConsumedAt,
    int ReferenceMonth,
    int ReferenceYear
);