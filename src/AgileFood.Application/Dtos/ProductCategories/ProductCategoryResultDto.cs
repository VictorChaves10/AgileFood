using AgileFood.Application.Dtos.Products;
using AgileFood.Business.Models.Products;

namespace AgileFood.Application.Dtos.ProductCategories;

public record ProductCategoryResultDto(
    int Id,
    string Name,
    IEnumerable<ProductResultDto>? Products
);
