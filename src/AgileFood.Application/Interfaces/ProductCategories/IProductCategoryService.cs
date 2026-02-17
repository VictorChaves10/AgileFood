using AgileFood.Application.Dtos.ProductCategories;

namespace AgileFood.Application.Interfaces.ProductCategories;

public interface IProductCategoryService
{
    Task<IEnumerable<ProductCategoryResultDto>> GetAllAsync();

    Task<ProductCategoryResultDto?> GetByIdAsync(int id);

    Task<ProductCategoryResultDto> CreateAsync(CreateProductCategoryDto categoryDto);

    Task<bool> UpdateAsync(UpdateProductCategoryDto categoryDto);

    Task<bool> DeleteAsync(int id);
}