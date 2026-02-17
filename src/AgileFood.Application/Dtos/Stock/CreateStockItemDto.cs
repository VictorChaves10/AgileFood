namespace AgileFood.Application.Dtos.Stock;

public record CreateStockItemDto(
    long ProductId,
    int InitialQuantity,
    DateTime? ExpirationDate
);

