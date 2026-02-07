using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Application.Interfaces;

public interface IStockItemService
{
    Task<StockItem> GetByIdAsync(int id);

    Task<IEnumerable<StockItem>> GetAllAsync();

}
