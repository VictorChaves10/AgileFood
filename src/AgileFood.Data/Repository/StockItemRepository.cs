using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Stock;
using AgileFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Data.Repository;

public class StockItemRepository : RepositoryBase<StockItem>, IStockItemRepository
{
    public StockItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<StockItem?> GetByIdAsync(long id)
    {
        return await _context.StockItems.AsNoTracking()
                                        .Include(x => x.Movements)
                                        .FirstOrDefaultAsync(x => x.Id == id);
    }
}
