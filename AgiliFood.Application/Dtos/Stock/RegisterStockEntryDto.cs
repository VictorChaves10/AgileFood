using System.ComponentModel.DataAnnotations;

namespace AgiliFood.Application.Dtos.Stock;

public record RegisterStockEntryDto(
    int Quantity,
    string? Reason
);