using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Application.Dtos.Stock;

public record StockItemResultDto(
    long Id,
    long ProductId,
    string? ProductName,
    int Quantity,
    DateTime? ExpirationDate,
    DateTime CreatedAt,
    IEnumerable<StockMovementResultDto> Movements
);

