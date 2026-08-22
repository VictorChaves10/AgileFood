using AgileFood.Business.Exceptions;
﻿using AgileFood.Business.Models.Consumptions;
using AgileFood.Business.Models.Products;

namespace AgileFood.Business.Models.Stock;

public class StockItem
{
    public long Id { get; private set; }

    public long ProductId { get; private set; }

    public Product? Product { get; private set; }

    public int Quantity { get; private set; }

    public DateTime? ExpirationDate { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public byte[] RowVersion { get; private set; } = Array.Empty<byte>();

    private readonly List<StockMovement> _movements = new();

    public IReadOnlyCollection<StockMovement> Movements => _movements;

    protected StockItem() { }

    public StockItem(long productId, int initialQuantity, DateTime? expirationDate = null)
    {
        ProductId = productId;
        CreatedAt = DateTime.UtcNow;
        SetExpirationDate(expirationDate);
        RegisterEntry(initialQuantity, StockMovementOrigin.InitialStock, "Estoque inicial");
    }

    public void RegisterEntry(int quantity, StockMovementOrigin origin = StockMovementOrigin.Manual, string reason = "Entrada")
    {
        if (quantity <= 0)
            throw new DomainException("A quantidade adicionada deve ser maior que zero.");

        Quantity += quantity;
        _movements.Add(new StockMovement(StockMovementType.Entry, origin, quantity, reason));
    }

    public void RegisterExit(int quantity, StockMovementOrigin origin = StockMovementOrigin.Manual, string reason = "Saída", Consumption? consumption = null)
    {
        if (quantity <= 0)
            throw new DomainException("A quantidade removida deve ser maior que zero.");

        if (Quantity < quantity)
            throw new DomainException("Quantidade em estoque insuficiente.");

        Quantity -= quantity;
        _movements.Add(new StockMovement(StockMovementType.Exit, origin, quantity, reason, consumption));
    }

    private void SetExpirationDate(DateTime? expirationDate)
    {
        if (expirationDate.HasValue && expirationDate.Value <= DateTime.UtcNow)
            throw new DomainException("Data de validade deve ser futura.");

        ExpirationDate = expirationDate;
    }
}
