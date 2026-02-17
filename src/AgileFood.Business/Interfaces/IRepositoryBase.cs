using System.Linq.Expressions;

namespace AgileFood.Business.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Delete(T entity);
    }
}
