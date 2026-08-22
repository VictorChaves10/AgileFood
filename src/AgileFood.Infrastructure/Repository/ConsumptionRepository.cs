using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Consumptions;
using AgileFood.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AgileFood.Infrastructure.Repository;

public class ConsumptionRepository : RepositoryBase<Consumption>, IConsumptionRepository
{
    public ConsumptionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Consumption>> GetByUserIdAsync(long userId)
    {
        return await _context.Consumptions
            .AsNoTracking()
            .Include(c => c.Items)
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.ConsumedAt)
            .ToListAsync();
    }
}
