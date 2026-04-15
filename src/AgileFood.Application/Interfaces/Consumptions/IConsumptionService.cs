using AgileFood.Application.Dtos.Consumptions;

namespace AgileFood.Application.Interfaces.Consumptions;

public interface IConsumptionService
{
    Task<ConsumptionResultDto> RegisterAsync(RegisterConsumptionDto dto);
    Task<IEnumerable<ConsumptionResultDto>> GetByUserIdAsync(long userId);
    Task<MonthlyBalanceResultDto?> GetMonthlyBalanceAsync(long userId, int month, int year);
}