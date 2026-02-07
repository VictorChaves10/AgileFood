using AgiliFood.Application.Dtos;
using AgiliFood.Application.Dtos.Products;

namespace AgiliFood.Application.Interfaces.Products;

public interface IProductService
{
    Task<IEnumerable<ProductResultDto>> GetAllAsync();
    Task<ProductResultDto?> GetByIdAsync(long id);
    Task<ProductResultDto> CreateAsync(CreateProductDto productDto);
    Task<ProductResultDto?> UpdateAsync(UpdateProductDto productDto);
    Task<bool> DeleteAsync(long id);
}
