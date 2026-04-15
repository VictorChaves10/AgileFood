using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Catalogs;
using AgileFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Data.Repository;

public class CatalogItemRepository : RepositoryBase<CatalogItem>, ICatalogItemRepository
{
    public CatalogItemRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<CatalogItem?> GetByIdAsync(long id)
    {
        return await _context.CatalogItems.AsNoTracking()
                             .Include(ci => ci.Product)
                                .ThenInclude(p => p!.ProductCategory)
                             .FirstOrDefaultAsync(ci => ci.Id == id);
    }

    public async Task<CatalogItem?> GetByProductIdAsync(long productId)
    {
        return await _context.CatalogItems
                             .FirstOrDefaultAsync(ci => ci.ProductId == productId);
    }

    public async Task<IEnumerable<CatalogItem>> GetAvailableItemsAsync()
    {
        return await _context.CatalogItems.AsNoTracking()
                             .Include(ci => ci.Product)
                                .ThenInclude(p => p!.ProductCategory)
                             .Where(ci => ci.IsAvailable && ci.Product!.IsActive)
                             .OrderBy(ci => ci.Product!.ProductCategory!.Name)
                             .ThenBy(ci => ci.Product!.Name)
                             .ToListAsync();
    }

    public async Task<IEnumerable<CatalogItem>> SearchAsync(string term)
    {
        var lowerTerm = term.ToLower();

        return await _context.CatalogItems.AsNoTracking()
                             .Include(ci => ci.Product)
                             .ThenInclude(p => p!.ProductCategory)
                             .Where(ci => ci.IsAvailable
                                       && ci.Product != null
                                       && ci.Product.IsActive
                                       && (ci.Product.Name.ToLower().Contains(lowerTerm)
                                        || (ci.Product.Description != null && ci.Product.Description.ToLower().Contains(lowerTerm))
                                        || (ci.Product.BarCode != null && ci.Product.BarCode.Contains(term))
                                        || (ci.Product.Brand != null && ci.Product.Brand.ToLower().Contains(lowerTerm))))
                             .OrderBy(ci => ci.Product!.ProductCategory!.Name)
                             .ThenBy(ci => ci.Product!.Name)
                             .ToListAsync();
    }
}