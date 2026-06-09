using AgileFood.Business.Models.Stock;

namespace AgileFood.Business.Interfaces;

public interface IStockItemRepository : IRepositoryBase<StockItem>
{
    Task<StockItem?> GetByIdAsync(long id);
    Task<IEnumerable<StockItem>> GetAvailableByProductIdAsync(long productId);
}
