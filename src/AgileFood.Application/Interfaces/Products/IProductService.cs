using AgileFood.Application.Dtos;
using AgileFood.Application.Dtos.Products;

namespace AgileFood.Application.Interfaces.Products;

public interface IProductService
{
    Task<IEnumerable<ProductResultDto>> GetAllAsync();
    Task<ProductResultDto?> GetByIdAsync(long id);
    Task<ProductResultDto> CreateAsync(CreateProductDto productDto);
    Task<bool> UpdateAsync(UpdateProductDto productDto);
    Task<bool> DeleteAsync(long id);
}
