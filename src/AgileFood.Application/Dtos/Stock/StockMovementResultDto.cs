using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Dtos.Stock;

public record StockMovementResultDto(
    long Id,
    StockMovementType Type,
    StockMovementOrigin Origin,
    int Quantity,
    string? Reason,
    long? ConsumptionId,
    DateTime Date
);