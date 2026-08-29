using AgileFood.Application.Dtos.Stock;
using AgileFood.Application.Interfaces.Stock;
using AgileFood.Application.Mappings.Stock;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Services.Stock;

public class StockItemService : IStockItemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TimeProvider _timeProvider;

    public StockItemService(IUnitOfWork unitOfWork, TimeProvider timeProvider)
    {
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
    }

    public async Task<StockItemResultDto> CreateAsync(CreateStockItemDto stockItemDto)
    {
        var stockItem = new StockItem(
            stockItemDto.ProductId,
            stockItemDto.InitialQuantity,
            _timeProvider.GetUtcNow().UtcDateTime,
            stockItemDto.ExpirationDate
        );

        _unitOfWork.StockItemRepository.Add(stockItem);
        await _unitOfWork.CommitAsync();

        return stockItem.MapToStockItemDto();
    }

    public async Task<IEnumerable<StockItemResultDto>> GetAllAsync()
    {
        var stockItems = await _unitOfWork.StockItemRepository.GetAllAsync();

        if (stockItems == null || !stockItems.Any())
            return Enumerable.Empty<StockItemResultDto>();

        return stockItems.Select(x => x.MapToStockItemDto());
    }

    public async Task<StockItemResultDto?> GetByIdAsync(long id)
    {
        var stockItem = await _unitOfWork.StockItemRepository.GetByIdAsync(id);

        if (stockItem == null) return null;

        return stockItem.MapToStockItemDto();
    }

    public async Task<bool> RegisterEntryAsync(long stockItemId, RegisterStockMovementDto dto)
    {
        var stockItem = await _unitOfWork.StockItemRepository.FindAsync(x => x.Id == stockItemId);

        if (stockItem == null)
            return false;

        stockItem.RegisterEntry(dto.Quantity, _timeProvider.GetUtcNow().UtcDateTime, StockMovementOrigin.Manual, dto.Reason);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> RegisterExitAsync(long stockItemId, RegisterStockMovementDto dto)
    {
        var stockItem = await _unitOfWork.StockItemRepository.FindAsync(x => x.Id == stockItemId);

        if (stockItem == null)
            return false;

        stockItem.RegisterExit(dto.Quantity, _timeProvider.GetUtcNow().UtcDateTime, StockMovementOrigin.Manual, dto.Reason);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
