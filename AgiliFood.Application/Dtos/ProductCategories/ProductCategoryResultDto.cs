using AgiliFood.Application.Dtos.Products;
using AgiliFood.Business.Models.Products;

namespace AgiliFood.Application.Dtos.ProductCategories;

public record ProductCategoryResultDto(
    int Id,
    string Name,
    IEnumerable<ProductResultDto>? Products
);
