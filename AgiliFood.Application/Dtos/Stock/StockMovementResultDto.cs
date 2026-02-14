using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Application.Dtos.Stock;

public record StockMovementResultDto(
    long Id,
    StockMovementType Type,
    int Quantity,
    string? Reason,
    DateTime Date
);