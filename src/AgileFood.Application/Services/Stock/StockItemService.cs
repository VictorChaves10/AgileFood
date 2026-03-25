using AgileFood.Application.Dtos.Stock;
using AgileFood.Application.Interfaces.Stock;
using AgileFood.Application.Mappings.Stock;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Services.Stock;

public class StockItemService : IStockItemService
{
    private readonly IUnitOfWork _unitOfWork;

    public StockItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StockItemResultDto> CreateAsync(CreateStockItemDto stockItemDto)
    {
        var stockItem = new StockItem
        (
            stockItemDto.ProductId,
            stockItemDto.InitialQuantity,
            stockItemDto.ExpirationDate
        );

        _unitOfWork.StockItemRepository.Create(stockItem);
        await _unitOfWork.CommitAsync();

        return stockItem.MapToStockItemDto();
    }

    public async Task<IEnumerable<StockItemResultDto?>> GetAllAsync()
    {
        var stockItens = await _unitOfWork.StockItemRepository.GetAllAsync();

        if (stockItens == null || !stockItens.Any())
            return Enumerable.Empty<StockItemResultDto?>();

        return stockItens.Select(x => x?.MapToStockItemDto());
    }

    public async Task<StockItemResultDto?> GetByIdAsync(long id)
    {
        var stock = await _unitOfWork.StockItemRepository.GetByIdAsync(id);

        if (stock == null) return null;

        return stock.MapToStockItemDto();
    }

    public async Task<bool> RegisterStockEntryAsync(long stockItemId, RegisterStockEntryDto entryDto)
    {
        var stockItem = await _unitOfWork.StockItemRepository.GetAsync(x => x.Id == stockItemId);

        if (stockItem == null)
            return false;

        stockItem.RegisterEntry(entryDto.Quantity, entryDto.Reason);
        await _unitOfWork.CommitAsync();

        return true;
    }


    public async Task<bool> RegisterStockExitAsync(long stockItemId, RegisterStockExitDto exitDto)
    {
        var stockItem = await _unitOfWork.StockItemRepository.GetAsync(x => x.Id == stockItemId);

        if (stockItem == null)
            return false;

        stockItem.RegisterExit(exitDto.Quantity, exitDto.Reason);
        await _unitOfWork.CommitAsync();

        return true;
    }

}
