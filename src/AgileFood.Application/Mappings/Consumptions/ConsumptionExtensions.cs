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
            consumption.GetTotalItems(),
            consumption.TotalPrice,
            consumption.ConsumedAt,
            consumption.ReferenceMonth,
            consumption.ReferenceYear,
            consumption.Items.Select(i => i.MapToConsumptionItemDto()).ToList()
        );
    }

    public static ConsumptionItemResultDto MapToConsumptionItemDto(this ConsumptionItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        return new ConsumptionItemResultDto(
            item.Id,
            item.ProductId,
            item.ProductName,
            item.Quantity,
            item.UnitPrice,
            item.TotalPrice
        );
    }
}
