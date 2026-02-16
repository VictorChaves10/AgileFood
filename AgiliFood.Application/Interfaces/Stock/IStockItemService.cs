using AgiliFood.Application.Dtos.Stock;
using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Application.Interfaces.Stock;

public interface IStockItemService
{
    Task<StockItemResultDto> CreateAsync(CreateStockItemDto stockItemDto);

    Task<StockItemResultDto?> GetByIdAsync(long id);

    Task<bool> AddStockEntryAsync(long stockItemId, RegisterStockEntryDto entryDto);

}
