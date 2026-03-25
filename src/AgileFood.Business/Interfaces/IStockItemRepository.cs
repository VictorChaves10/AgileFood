using AgileFood.Business.Models.Stock;
using System.Linq.Expressions;

namespace AgileFood.Business.Interfaces;

public interface IStockItemRepository
{   
    void Create(StockItem item);

    Task<StockItem?> GetByIdAsync(long id);
    Task<StockItem?> GetAsync(Expression<Func<StockItem, bool>> predicate);
    Task<IEnumerable<StockItem?>> GetAllAsync();

}
