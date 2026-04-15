using AgileFood.Application.Dtos.Consumptions;
using AgileFood.Application.Interfaces.Consumptions;
using AgileFood.Application.Mappings.Consumptions;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Consumptions;
using AgileFood.Business.Models.Stock;

namespace AgileFood.Application.Services.Consumptions;

public class ConsumptionService : IConsumptionService
{
    private readonly IUnitOfWork _unitOfWork;

    public ConsumptionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ConsumptionResultDto> RegisterAsync(RegisterConsumptionDto dto)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);
        if (product is null)
            throw new InvalidOperationException("Produto não encontrado.");

        if (!product.IsActive)
            throw new InvalidOperationException("Produto não está ativo.");

        var user = await _unitOfWork.UserRepository.GetByIdAsync(dto.UserId);
        if (user is null)
            throw new InvalidOperationException("Usuário não encontrado.");

        if (!user.IsActive)
            throw new InvalidOperationException("Usuário não está ativo.");

        var stockItem = await _unitOfWork.StockItemRepository
            .FindAsync(s => s.ProductId == dto.ProductId && s.Quantity >= dto.Quantity);

        if (stockItem is null)
            throw new InvalidOperationException("Estoque insuficiente para este produto.");

        var consumption = new Consumption(
            dto.UserId,
            dto.ProductId,
            dto.Quantity,
            product.Price
        );

        // Persiste primeiro o consumo para obter o Id
        _unitOfWork.ConsumptionRepository.Add(consumption);
        await _unitOfWork.CommitAsync();

        // Registra a saída com vínculo ao consumo
        stockItem.RegisterExit(
            dto.Quantity,
            StockMovementOrigin.Consumption,
            $"Consumo #{consumption.Id} - {user.Name}",
            consumption.Id
        );

        await _unitOfWork.CommitAsync();

        return consumption.MapToConsumptionDto();
    }

    public async Task<IEnumerable<ConsumptionResultDto>> GetByUserIdAsync(long userId)
    {
        var consumptions = await _unitOfWork.ConsumptionRepository.GetByUserIdAsync(userId);
        return consumptions.Select(c => c.MapToConsumptionDto());
    }

    public async Task<MonthlyBalanceResultDto?> GetMonthlyBalanceAsync(long userId, int month, int year)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null) return null;

        var consumptions = await _unitOfWork.ConsumptionRepository
            .GetByUserAndMonthAsync(userId, month, year);

        var items = consumptions.Select(c => c.MapToConsumptionDto()).ToList();

        return new MonthlyBalanceResultDto(
            userId,
            user.Name,
            month,
            year,
            items.Sum(c => c.Quantity),
            items.Sum(c => c.TotalPrice),
            items
        );
    }
}