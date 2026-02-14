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

    public void CreateStockItem(StockItem item)
    {
        _context.StockItems.Add(item);
    }
}
