using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Consumptions;
using AgileFood.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Data.Repository;

public class ConsumptionRepository : RepositoryBase<Consumption>, IConsumptionRepository
{
    public ConsumptionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Consumption?> GetByIdAsync(long id)
    {
        return await _context.Consumptions.AsNoTracking()
                             .Include(c => c.Product)
                             .Include(c => c.User)
                             .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Consumption>> GetByUserIdAsync(long userId)
    {
        return await _context.Consumptions.AsNoTracking()
                             .Include(c => c.Product)
                             .Where(c => c.UserId == userId)
                             .OrderByDescending(c => c.ConsumedAt)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Consumption>> GetByUserAndMonthAsync(long userId, int month, int year)
    {
        return await _context.Consumptions.AsNoTracking()
                             .Include(c => c.Product)
                             .Where(c => c.UserId == userId
                                      && c.ReferenceMonth == month
                                      && c.ReferenceYear == year)
                             .OrderByDescending(c => c.ConsumedAt)
                             .ToListAsync();
    }
}