using AgiliFood.Business.Interfaces;
using AgiliFood.Business.Models.Stock;
using AgiliFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgiliFood.Data.Repository;

public class StockItemRepository : IStockItemRepository
{
    private readonly ApplicationDbContext _context;

    public StockItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StockItem>> GetAllAsync()
    {
        var stockItems = await _context.StockItems.AsNoTracking()
                                                  .ToListAsync();

        return stockItems;
    }

    public Task<StockItem> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
