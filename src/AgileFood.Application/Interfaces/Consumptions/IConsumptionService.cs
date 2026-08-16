using AgileFood.Application.Dtos.Consumptions;

namespace AgileFood.Application.Interfaces.Consumptions;

public interface IConsumptionService
{
    Task<ConsumptionResultDto> RegisterConsumptionAsync(RegisterConsumptionDto dto);
    Task<IEnumerable<ConsumptionResultDto>> GetByUserAsync(long userId);
    Task<IEnumerable<MonthlyConsumptionSummaryDto>> GetMonthlySummaryByUserAsync(long userId);
}
