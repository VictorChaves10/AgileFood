using AgiliFood.Business.Interfaces;
using AgiliFood.Business.Models.Products;
using AgiliFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgiliFood.Data.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<Product?> GetByIdAsync(long id)
    {
        return await _context.Products.AsNoTracking()
                                .Include(x => x.ProductCategory)
                                .FirstOrDefaultAsync(x => x.Id == id);
    }
}

