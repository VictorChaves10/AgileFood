using AgiliFood.Application.Dtos;
using AgiliFood.Application.Dtos.Products;
using AgiliFood.Application.Interfaces.Products;
using AgiliFood.Application.Mappings.Products;
using AgiliFood.Business.Interfaces;
using AgiliFood.Business.Models.Products;

namespace AgiliFood.Application.Services.Products;

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

        return products.Select(p => new ProductResultDto
        (
              p.Id,
              p.Name,
              p.Description,
              p.Brand,
              p.Flavor,
              p.Weight.Amount,
              p.Weight.Unit,
              p.Price,
              p.IsActive,
              p.BarCode,
              p.ProductCategoryId,
              p.Image

        )).ToList();
    }

    public async Task<ProductResultDto?> GetByIdAsync(long id)
    {
        var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id);

        if (product == null)
            return null;

        return product.MapToProductDto();
    }

    public async Task<ProductResultDto?> UpdateAsync(ProductDto productDto)
    {
        var product = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == productDto.Id);

        if (product == null)
            return null;

        product.ChangeName(productDto.Name);
        product.ChangeFlavor(productDto.Flavor);
        product.ChangeWeight(productDto.WeightAmount, productDto.WeightUnit);
        product.ChangePrice(productDto.Price);
        product.ChangeCategory(productDto.ProductCategoryId);
        product.ChangeImage(productDto.Image);

        if (productDto.IsActive)
            product.Activate();
        else
            product.Deactivate();

        await _unitOfWork.CommitAsync();

        return product.MapToProductDto();
    }
}
