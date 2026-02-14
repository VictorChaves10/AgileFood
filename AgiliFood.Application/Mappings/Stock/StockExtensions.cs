using AgiliFood.Application.Dtos.Stock;
using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Application.Mappings.Stock;

internal static class StockExtensions
{
    public static StockMovementResultDto MapToStockMovementDto(this StockMovement movement)
    {
        ArgumentNullException.ThrowIfNull(movement);

        return new StockMovementResultDto(
            movement.Id,
            movement.Type,
            movement.Quantity,
            movement.Reason,
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