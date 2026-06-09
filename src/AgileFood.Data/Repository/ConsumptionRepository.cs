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
}
