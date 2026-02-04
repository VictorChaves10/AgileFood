using AgiliFood.Application.Interfaces;
using AgiliFood.Business.Interfaces;

namespace AgiliFood.Application.Services;

public class StockItemService : IStockItemService
{
    private readonly IUnitOfWork _unitOfWork;

    public StockItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<StockItem>> GetAllAsync()
    {
        var stockItems = await _unitOfWork.StockItemRepository.GetAllAsync();
        return stockItems;
    }

    public Task<StockItem> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
