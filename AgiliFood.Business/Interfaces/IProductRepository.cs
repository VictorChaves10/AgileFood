using AgiliFood.Business.Models.Products;

namespace AgiliFood.Business.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product?> GetByIdAsync(long id);
    }
}
