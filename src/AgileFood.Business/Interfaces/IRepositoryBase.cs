using System.Linq.Expressions;

namespace AgileFood.Business.Interfaces;

public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}
