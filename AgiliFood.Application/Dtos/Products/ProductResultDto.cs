using AgiliFood.Business.Models.Weights;

namespace AgiliFood.Application.Dtos.Products
{
    public record ProductResultDto(
        long Id,
        string Name,
        string? Description,
        string? Brand,
        string? Flavor,
        decimal WeightAmount,
        WeightUnitEnum WeightUnit,
        decimal Price,
        bool IsActive,
        string? BarCode,
        int ProductCategoryId,
        string? Image,
        string? CategoryName
    );
}
