using AgileFood.Business.Models.Weights;

namespace AgileFood.Application.Dtos.Products;

public record CreateProductDto(

    string Name,
    string? Description,
    string? Brand,
    string? Flavor,
    bool IsActive,
    decimal WeightAmount,
    WeightUnitEnum WeightUnit,
    decimal Price,
    string BarCode,
    int ProductCategoryId,
    string? Image,
    string? ImageUpload
);
