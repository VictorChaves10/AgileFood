using AgiliFood.Application.Dtos.ProductCategories;
using AgiliFood.Application.Interfaces.ProductCategories;
using AgiliFood.Application.Mappings.ProductCategories;
using AgiliFood.Application.Mappings.Products;
using AgiliFood.Business.Interfaces;
using AgiliFood.Business.Models.Products;

namespace AgiliFood.Application.Services.ProductCategories;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ProductCategoryResultDto> CreateAsync(CreateProductCategoryDto categoryDto)
    {
        var category = new ProductCategory(categoryDto.Name);
        _unitOfWork.ProductCategoryRepository.Create(category);

        await _unitOfWork.CommitAsync();

        return category.MapToProductCategoryDto();
    }

    public async Task<ProductCategoryResultDto?> GetByIdAsync(int id)
    {
        var category = await _unitOfWork.ProductCategoryRepository.GetByIdAsync(id);

        if (category == null) return null;

        return category.MapToProductCategoryDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _unitOfWork.ProductCategoryRepository.GetAsync(x => x.Id == id);

        if (category == null)
            return false;

        _unitOfWork.ProductCategoryRepository.Delete(category);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(UpdateProductCategoryDto categoryDto)
    {
        var category = await _unitOfWork.ProductCategoryRepository.GetAsync(x => x.Id == categoryDto.Id);

        if (category == null) return false;

        category.ChangeName(categoryDto.Name);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<IEnumerable<ProductCategoryResultDto>> GetAllAsync()
    {
        var categories = await _unitOfWork.ProductCategoryRepository.GetAllAsync();

        if (categories == null || !categories.Any()) 
            return Enumerable.Empty<ProductCategoryResultDto>();

        return categories.Select(x => x.MapToProductCategoryDto());
    }
}
