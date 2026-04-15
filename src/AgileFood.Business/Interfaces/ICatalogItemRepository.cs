using AgileFood.Business.Models.Catalogs;

namespace AgileFood.Business.Interfaces;

public interface ICatalogItemRepository : IRepositoryBase<CatalogItem>
{
    Task<CatalogItem?> GetByIdAsync(long id);
    Task<CatalogItem?> GetByProductIdAsync(long productId);
    Task<IEnumerable<CatalogItem>> GetAvailableItemsAsync();
    Task<IEnumerable<CatalogItem>> SearchAsync(string term);
}