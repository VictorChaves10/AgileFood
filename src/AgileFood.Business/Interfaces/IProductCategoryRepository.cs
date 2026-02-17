using AgileFood.Business.Models.Products;

namespace AgileFood.Business.Interfaces
{
    public interface IProductCategoryRepository : IRepositoryBase<ProductCategory>
    {
        Task<ProductCategory?> GetByIdAsync(int id);


    }
}
