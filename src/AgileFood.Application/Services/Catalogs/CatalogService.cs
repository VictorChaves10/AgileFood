using AgileFood.Application.Dtos.Catalogs;
using AgileFood.Application.Interfaces.Catalogs;
using AgileFood.Application.Mappings.Catalogs;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Catalogs;

namespace AgileFood.Application.Services.Catalogs;

public class CatalogService : ICatalogService
{
    private readonly IUnitOfWork _unitOfWork;

    public CatalogService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CatalogItemResultDto>> GetAvailableItemsAsync()
    {
        var items = await _unitOfWork.CatalogItemRepository.GetAvailableItemsAsync();
        return items.Select(i => i.MapToCatalogItemDto());
    }

    public async Task<IEnumerable<CatalogItemResultDto>> SearchAsync(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return await GetAvailableItemsAsync();

        var items = await _unitOfWork.CatalogItemRepository.SearchAsync(term);
        return items.Select(i => i.MapToCatalogItemDto());
    }

    public async Task<CatalogItemResultDto> CreateAsync(CreateCatalogItemDto dto)
    {
        var existing = await _unitOfWork.CatalogItemRepository.GetByProductIdAsync(dto.ProductId);
        if (existing is not null)
            throw new InvalidOperationException("Este produto já está no catálogo.");

        var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
        if (product is null)
            throw new InvalidOperationException("Produto não encontrado.");

        var catalogItem = new CatalogItem(dto.ProductId);

        _unitOfWork.CatalogItemRepository.Add(catalogItem);
        await _unitOfWork.CommitAsync();

        return catalogItem.MapToCatalogItemDto();
    }

    public async Task<bool> UpdateAsync(UpdateCatalogItemDto dto)
    {
        var catalogItem = await _unitOfWork.CatalogItemRepository
            .FindAsync(ci => ci.Id == dto.Id);

        if (catalogItem is null) return false;

        if (dto.IsAvailable)
            catalogItem.MakeAvailable();
        else
            catalogItem.MakeUnavailable();

        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var catalogItem = await _unitOfWork.CatalogItemRepository
            .FindAsync(ci => ci.Id == id);

        if (catalogItem is null) return false;

        _unitOfWork.CatalogItemRepository.Remove(catalogItem);
        await _unitOfWork.CommitAsync();
        return true;
    }
}