using AgileFood.Application.Dtos;
using AgileFood.Application.Dtos.Products;
using AgileFood.Application.Interfaces.Products;
using AgileFood.Application.Mappings.Products;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Products;

namespace AgileFood.Application.Services.Products;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductResultDto> CreateAsync(CreateProductDto productDto)
    {
        var product = new Product(
            productDto.Name,
            productDto.Description,
            productDto.Brand,
            productDto.Flavor,
            productDto.Price,
            productDto.IsActive,
            productDto.BarCode,
            productDto.Image,
            productDto.ProductCategoryId,
            productDto.WeightAmount,
            productDto.WeightUnit
        );

        _unitOfWork.ProductRepository.Create(product);
        await _unitOfWork.CommitAsync();

        return product.MapToProductDto();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id);

        if (entity is null)
            return false;

        _unitOfWork.ProductRepository.Delete(entity);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<IEnumerable<ProductResultDto>> GetAllAsync()
    {
        var products = await _unitOfWork.ProductRepository.GetAllAsync();

        if (products == null || !products.Any())
            return Enumerable.Empty<ProductResultDto>();

        return products.Select(p => p.MapToProductDto());   
    }

    public async Task<ProductResultDto?> GetByIdAsync(long id)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

        if (product == null)
            return null;

        return product.MapToProductDto();
    }

    public async Task<bool> UpdateAsync(UpdateProductDto productDto)
    {
        var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == productDto.Id);

        if (product == null)
            return false;

        product.Update(
            productDto.Name,
            productDto.Flavor,
            productDto.Brand,
            productDto.BarCode,
            productDto.Price,
            productDto.ProductCategoryId,
            productDto.WeightAmount,
            productDto.WeightUnit,
            productDto.Image,
            productDto.IsActive,
            productDto.Description
            );

        await _unitOfWork.CommitAsync();
        return true;
    }
}
