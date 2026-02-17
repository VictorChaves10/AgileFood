using AgileFood.Application.Dtos.ProductCategories;
using AgileFood.Application.Dtos.Products;
using AgileFood.Application.Mappings.Products;
using AgileFood.Business.Models.Products;

namespace AgileFood.Application.Mappings.ProductCategories;

internal static class ProductCategoryExtensions
{
    public static ProductCategoryResultDto MapToProductCategoryDto(this ProductCategory category)
    {
        ArgumentNullException.ThrowIfNull(category);

        return new ProductCategoryResultDto(
                        category.Id,
                        category.Name,
                        category.Products?.Select(x => x.MapToProductDto()) ?? Enumerable.Empty<ProductResultDto>()
                   );
    }

}
