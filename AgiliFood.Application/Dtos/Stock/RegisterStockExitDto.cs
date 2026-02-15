namespace AgiliFood.Application.Dtos.Stock;

public record RegisterStockExitDto(
    int Quantity,
    string? Reason
);