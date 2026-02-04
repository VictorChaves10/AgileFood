namespace AgiliFood.Business.Interfaces;

public interface IStockItemRepository
{   

    Task<StockItem> GetByIdAsync(long id);

    Task<IEnumerable<StockItem>> GetAllAsync();


}
