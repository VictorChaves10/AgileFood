using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Business.Interfaces;

public interface IStockItemRepository
{   
    void CreateStockItem(StockItem item);

    Task<StockItem?> GetByIdAsync(long id);

}
