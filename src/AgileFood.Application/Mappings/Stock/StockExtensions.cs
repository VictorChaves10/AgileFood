using AgileFood.Application.Dtos.Stock;
using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Mappings.Stock;

public static class StockExtensions
{
    public static StockMovementResultDto MapToStockMovementDto(this StockMovement movement)
    {
        ArgumentNullException.ThrowIfNull(movement);

        return new StockMovementResultDto(
            movement.Id,
            movement.Type,
            movement.Origin,
            movement.Quantity,
            movement.Reason,
            movement.ConsumptionId,
            movement.Date
        );
    }

    public static StockItemResultDto MapToStockItemDto(this StockItem stockItem)
    {
        ArgumentNullException.ThrowIfNull(stockItem);

        return new StockItemResultDto(
            stockItem.Id,
            stockItem.ProductId,
            stockItem.Product?.Name,
            stockItem.Quantity,
            stockItem.ExpirationDate,
            stockItem.CreatedAt,
            stockItem.Movements.Select(MapToStockMovementDto)
        );
    }
}