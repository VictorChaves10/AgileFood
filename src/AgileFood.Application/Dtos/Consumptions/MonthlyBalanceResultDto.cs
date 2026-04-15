namespace AgileFood.Application.Dtos.Consumptions;

public record MonthlyBalanceResultDto(
    long UserId,
    string UserName,
    int Month,
    int Year,
    int TotalItems,
    decimal TotalAmount,
    IEnumerable<ConsumptionResultDto> Consumptions
);