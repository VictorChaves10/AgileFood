using AgiliFood.Application.Dtos.Stock;
using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Application.Interfaces.Stock;

public interface IStockItemService
{
    Task<StockItemResultDto> CreateStockItem(CreateStockItemDto stockItemDto);

    Task<StockItemResultDto?> GetById(long id);
}
