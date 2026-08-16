namespace AgileFood.Application.Dtos.Consumptions;

public record MonthlyConsumptionSummaryDto(
    int ReferenceYear,
    int ReferenceMonth,
    decimal TotalAmount,
    int ConsumptionCount
);
