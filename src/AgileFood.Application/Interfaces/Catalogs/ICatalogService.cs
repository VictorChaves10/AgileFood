using AgileFood.Application.Dtos.Catalogs;

namespace AgileFood.Application.Interfaces.Catalogs;

public interface ICatalogService
{
    Task<IEnumerable<CatalogItemResultDto>> GetAvailableItemsAsync();
    Task<IEnumerable<CatalogItemResultDto>> SearchAsync(string term);
    Task<CatalogItemResultDto> CreateAsync(CreateCatalogItemDto dto);
    Task<bool> UpdateAsync(UpdateCatalogItemDto dto);
    Task<bool> DeleteAsync(long id);
}