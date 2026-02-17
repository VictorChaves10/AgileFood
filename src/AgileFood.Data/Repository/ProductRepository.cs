using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Products;
using AgileFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Data.Repository;

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

