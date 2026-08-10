using AgileFood.Application.Dtos.Consumptions;
using AgileFood.Application.Interfaces.Consumptions;
using AgileFood.Application.Mappings.Consumptions;
using AgileFood.Business.Interfaces;
using AgileFood.Business.Models.Consumptions;
using AgileFood.Business.Models.Stock;
using AgileFood.Business.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace AgileFood.Application.Services.Consumptions;

public class ConsumptionService : IConsumptionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly PasswordHasher<User> _passwordHasher;

    public ConsumptionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<ConsumptionResultDto> RegisterConsumptionAsync(RegisterConsumptionDto dto)
    {
        var user = await ValidateUserAndPinAsync(dto.Cpf, dto.Pin);
        return await RegisterCartForUserAsync(user, dto.Items);
    }

    private async Task<User> ValidateUserAndPinAsync(string cpf, string pin)
    {
        var normalizedCpf = User.NormalizeCpf(cpf);
        var user = await _unitOfWork.UserRepository.GetByCpfAsync(normalizedCpf);

        if (user is null)
            throw new InvalidOperationException("Usuário não encontrado.");

        if (!user.IsActive)
            throw new InvalidOperationException("Usuário não está ativo.");

        if (string.IsNullOrWhiteSpace(pin) || pin.Length != 4 || !pin.All(char.IsDigit))
            throw new InvalidOperationException("PIN invalido.");

        var verification = _passwordHasher.VerifyHashedPassword(user, user.TransactionPinHash, pin);

        if (verification == PasswordVerificationResult.Failed)
            throw new InvalidOperationException("PIN invalido.");

        return user;
    }

    private async Task<ConsumptionResultDto> RegisterCartForUserAsync(User user, IReadOnlyCollection<RegisterConsumptionItemDto> items)
    {
        if (items is null || items.Count == 0)
            throw new InvalidOperationException("O carrinho está vazio.");

        var groupedItems = items
            .GroupBy(i => i.ProductId)
            .Select(g => new RegisterConsumptionItemDto(g.Key, g.Sum(i => i.Quantity)))
            .ToList();

        if (groupedItems.Any(i => i.ProductId <= 0 || i.Quantity <= 0))
            throw new InvalidOperationException("O carrinho possui itens invalidos.");

        var consumption = new Consumption(user.Id);


        foreach (var item in groupedItems)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
            if (product is null)
                throw new InvalidOperationException("Produto nao encontrado.");

            if (!product.IsActive)
                throw new InvalidOperationException($"Produto '{product.Name}' nao esta ativo.");

            var stockItems = (await _unitOfWork.StockItemRepository.GetAvailableByProductIdAsync(item.ProductId)).ToList();
            if (stockItems.Sum(s => s.Quantity) < item.Quantity)
                throw new InvalidOperationException($"Estoque insuficiente para '{product.Name}'.");

            consumption.AddItem(product.Id, product.Name, product.Price, item.Quantity);
        }

        _unitOfWork.ConsumptionRepository.Add(consumption);

        foreach (var item in consumption.Items)
        {
            var remainingQuantity = item.Quantity;
            var stockItems = (await _unitOfWork.StockItemRepository.GetAvailableByProductIdAsync(item.ProductId)).ToList();

            foreach (var stockItem in stockItems)
            {
                if (remainingQuantity == 0)
                    break;

                var quantityToRemove = Math.Min(stockItem.Quantity, remainingQuantity);

                stockItem.RegisterExit(
                    quantityToRemove,
                    StockMovementOrigin.Consumption,
                    $"Consumo #{user.Name} - {item.ProductName}",
                    consumption
                );

                remainingQuantity -= quantityToRemove;
            }

            if (remainingQuantity > 0)
                throw new InvalidOperationException($"Estoque insuficiente para '{item.ProductName}'.");
        }

        await _unitOfWork.CommitAsync();
        return consumption.MapToConsumptionDto();
    }
}
