using AgiliFood.Business.Interfaces;
using AgiliFood.Business.Models.Products;
using AgiliFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgiliFood.Data.Repository;

public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ProductCategory?> GetByIdAsync(int id)
    {
       return await _context.ProductCategories.AsNoTracking()
                                              .Include(pc => pc.Products)
                                              .FirstOrDefaultAsync(pc => pc.Id == id);
    }
}
