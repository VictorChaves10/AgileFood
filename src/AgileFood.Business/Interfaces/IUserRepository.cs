using System.Linq.Expressions;
using AgileFood.Business.Models.Users;

namespace AgileFood.Business.Interfaces;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
}