using AgileFood.Application.Dtos.Products;
using AgileFood.Business.Models.Products;

namespace AgileFood.Application.Mappings.Products;

internal static class ProductExtensions
{
    public static ProductResultDto MapToProductDto(this Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        return new ProductResultDto(
              product.Id,
              product.Name,
              product.Description,
              product.Brand,
              product.Flavor,
              product.Weight.Amount,
              product.Weight.Unit,
              product.Price,
              product.IsActive,
              product.BarCode,
              product.ProductCategoryId,
              product.Image,
              product.ProductCategory?.Name
          );
    }


}
