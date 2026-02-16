using AgiliFood.Business.Models.Stock;
using System.Linq.Expressions;

namespace AgiliFood.Business.Interfaces;

public interface IStockItemRepository
{   
    void Create(StockItem item);

    Task<StockItem?> GetByIdAsync(long id);

    Task<StockItem?> GetAsync(Expression<Func<StockItem, bool>> predicate);

}
