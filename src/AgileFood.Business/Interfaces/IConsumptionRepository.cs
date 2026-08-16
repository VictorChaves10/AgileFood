using AgileFood.Business.Models.Consumptions;

namespace AgileFood.Business.Interfaces;

public interface IConsumptionRepository : IRepositoryBase<Consumption>
{
    Task<IEnumerable<Consumption>> GetByUserIdAsync(long userId);
}
