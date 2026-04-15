using AgileFood.Business.Models.Consumptions;

namespace AgileFood.Business.Interfaces;

public interface IConsumptionRepository : IRepositoryBase<Consumption>
{
    Task<Consumption?> GetByIdAsync(long id);
    Task<IEnumerable<Consumption>> GetByUserIdAsync(long userId);
    Task<IEnumerable<Consumption>> GetByUserAndMonthAsync(long userId, int month, int year);
}