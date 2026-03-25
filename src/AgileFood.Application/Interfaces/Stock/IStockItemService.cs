using AgileFood.Application.Dtos.Stock;
using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Interfaces.Stock;

public interface IStockItemService
{
    Task<IEnumerable<StockItemResultDto?>> GetAllAsync();
    Task<StockItemResultDto?> GetByIdAsync(long id);
    Task<StockItemResultDto> CreateAsync(CreateStockItemDto stockItemDto);
    Task<bool> RegisterStockEntryAsync(long stockItemId, RegisterStockEntryDto entryDto);
    Task<bool> RegisterStockExitAsync(long stockItemId, RegisterStockExitDto entryDto);

}
