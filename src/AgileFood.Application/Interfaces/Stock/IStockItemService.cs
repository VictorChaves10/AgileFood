using AgileFood.Application.Dtos.Stock;

namespace AgileFood.Application.Interfaces.Stock;

public interface IStockItemService
{
    Task<IEnumerable<StockItemResultDto>> GetAllAsync();
    Task<StockItemResultDto?> GetByIdAsync(long id);
    Task<StockItemResultDto> CreateAsync(CreateStockItemDto stockItemDto);
    Task<bool> RegisterEntryAsync(long stockItemId, RegisterStockMovementDto dto);
    Task<bool> RegisterExitAsync(long stockItemId, RegisterStockMovementDto dto);
}
