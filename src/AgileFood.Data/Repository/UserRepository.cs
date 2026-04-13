using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Users;
using AgileFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Data.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _context.Users.AsNoTracking()
                             .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
                             .FirstOrDefaultAsync(u => u.Email == email);
    }
}