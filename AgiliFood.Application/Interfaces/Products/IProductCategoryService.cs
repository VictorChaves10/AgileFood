using AgiliFood.Application.Dtos.Products;

namespace AgiliFood.Application.Interfaces.Products;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
    Task<ProductCategoryDto> GetByIdAsync(int id);
    Task<ProductCategoryDto> CreateAsync(ProductCategoryDto productCategoryDto);
    Task<ProductCategoryDto> UpdateAsync(ProductCategoryDto productCategoryDto);
    Task<bool> DeleteAsync(int id);    
    Task<ProductCategoryDto> GetAllWithProductsAsync(int id);
}