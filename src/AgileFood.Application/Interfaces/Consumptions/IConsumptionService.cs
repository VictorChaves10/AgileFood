using AgileFood.Application.Dtos.Consumptions;

namespace AgileFood.Application.Interfaces.Consumptions;

public interface IConsumptionService
{
    Task<ConsumptionResultDto> RegisterConsumptionAsync(RegisterConsumptionDto dto);
}
