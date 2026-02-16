using AgiliFood.Business.Interfaces;
using AgiliFood.Business.Models.Stock;
using AgiliFood.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgiliFood.Data.Repository;

public class StockItemRepository : IStockItemRepository
{
    private readonly ApplicationDbContext _context;

    public StockItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Create(StockItem item)
    {
        _context.StockItems.Add(item);
    }

    public async Task<StockItem?> GetAsync(Expression<Func<StockItem, bool>> predicate)
    {
        return await _context.StockItems.FirstOrDefaultAsync(predicate);
    }

    public async Task<StockItem?> GetByIdAsync(long id)
    {
        return await _context.StockItems.AsNoTracking()
                                        .Include(x => x.Movements)
                                        .FirstOrDefaultAsync(x => x.Id == id);
    }
}
