using AgileFood.Application.Dtos.Consumptions;
using AgileFood.Business.Models.Consumptions;

namespace AgileFood.Application.Mappings.Consumptions;

public static class ConsumptionExtensions
{
    public static ConsumptionResultDto MapToConsumptionDto(this Consumption consumption)
    {
        ArgumentNullException.ThrowIfNull(consumption);

        return new ConsumptionResultDto(
            consumption.Id,
            consumption.UserId,
            consumption.User?.Name,
            consumption.ProductId,
            consumption.Product?.Name,
            consumption.Quantity,
            consumption.UnitPrice,
            consumption.TotalPrice,
            consumption.ConsumedAt,
            consumption.ReferenceMonth,
            consumption.ReferenceYear
        );
    }
}