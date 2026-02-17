using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Products;
using AgileFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Data.Repository;

public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<ProductCategory?> GetByIdAsync(int id)
    {
       return await _context.ProductCategories.AsNoTracking()
                                              .FirstOrDefaultAsync(x => x.Id == id);
    }
}
