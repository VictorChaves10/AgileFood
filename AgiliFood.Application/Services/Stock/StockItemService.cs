using AgiliFood.Application.Dtos.Stock;
using AgiliFood.Application.Interfaces.Stock;
using AgiliFood.Application.Mappings.Stock;
using AgiliFood.Business.Interfaces;
using AgiliFood.Business.Models.Stock;

namespace AgiliFood.Application.Services.Stock;

public class StockItemService : IStockItemService
{
    private readonly IUnitOfWork _unitOfWork;

    public StockItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StockItemResultDto> CreateStockItem(CreateStockItemDto stockItemDto)
    {
        var stockItem = new StockItem
        (
            stockItemDto.ProductId,
            stockItemDto.InitialQuantity,
            stockItemDto.ExpirationDate
        );

        _unitOfWork.StockItemRepository.CreateStockItem(stockItem);
        await _unitOfWork.CommitAsync();

        return stockItem.MapToStockItemDto();
    }

    public async Task<StockItemResultDto?> GetById(long id)
    {
        var stock = await _unitOfWork.StockItemRepository.GetByIdAsync(id);

        if (stock == null) return null;

        return stock.MapToStockItemDto();
    }
}
