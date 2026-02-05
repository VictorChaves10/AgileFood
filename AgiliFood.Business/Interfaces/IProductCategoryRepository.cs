using AgiliFood.Business.Models.Products;

namespace AgiliFood.Business.Interfaces
{
    public interface IProductCategoryRepository : IRepositoryBase<ProductCategory>
    {
        Task<ProductCategory> GetAllWithProductsAsync(int id);
    }
}
