using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Dtos.Stock;

public record StockMovementResultDto(
    long Id,
    StockMovementType Type,
    int Quantity,
    string? Reason,
    DateTime Date
);