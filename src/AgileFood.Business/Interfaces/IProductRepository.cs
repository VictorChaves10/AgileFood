using AgileFood.Business.Models.Products;

namespace AgileFood.Business.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product?> GetByIdAsync(long id);
    }
}
