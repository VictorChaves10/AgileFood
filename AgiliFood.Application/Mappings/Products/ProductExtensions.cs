using AgiliFood.Application.Dtos.Products;
using AgiliFood.Business.Models.Products;

namespace AgiliFood.Application.Mappings.Products
{
    internal static class ProductExtensions
    {
        public static ProductResultDto MapToProductDto(this Product product)
        {
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
                  product.Image
              )
            {
                CategoryName = product.ProductCategory?.Name,
            };
        }
    }
}
