using AgiliFood.Application.Dtos.Products;
using AgiliFood.Application.Interfaces.Products;
using AgiliFood.Business.Interfaces;

namespace AgiliFood.Application.Services.Products;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductCategoryDto> CreateAsync(ProductCategoryDto productCategoryDto)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductCategoryDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ProductCategoryDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductCategoryDto> GetAllWithProductsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ProductCategoryDto> UpdateAsync(ProductCategoryDto dto)
    {
        throw new NotImplementedException();
    }

}
