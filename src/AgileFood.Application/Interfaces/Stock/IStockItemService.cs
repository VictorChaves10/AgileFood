using AgileFood.Application.Dtos.Stock;
using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Interfaces.Stock;

public interface IStockItemService
{
    Task<StockItemResultDto> CreateAsync(CreateStockItemDto stockItemDto);

    Task<StockItemResultDto?> GetByIdAsync(long id);

    Task<bool> AddStockEntryAsync(long stockItemId, RegisterStockEntryDto entryDto);

}
