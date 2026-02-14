using AgiliFood.Application.Dtos.ProductCategories;
using AgiliFood.Application.Dtos.Products;
using AgiliFood.Application.Mappings.Products;
using AgiliFood.Business.Models.Products;

namespace AgiliFood.Application.Mappings.ProductCategories;

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
