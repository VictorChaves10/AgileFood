using System.ComponentModel.DataAnnotations;

namespace AgileFood.Application.Dtos.Stock;

public record RegisterStockEntryDto(
    int Quantity,
    string? Reason
);