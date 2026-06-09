namespace AgileFood.Application.Dtos.Consumptions;

public record ConsumptionResultDto(
    long Id,
    long UserId,
    string? UserName,
    int TotalItems,
    decimal TotalPrice,
    DateTime ConsumedAt,
    int ReferenceMonth,
    int ReferenceYear,
    IEnumerable<ConsumptionItemResultDto> Items
);
