namespace AgileFood.Application.Dtos.Stock;

public record RegisterStockMovementDto(
    int Quantity,
    string Reason
);