using AgileFood.Business.Models.Products;
using AgileFood.Business.Models.Stock;

namespace AgileFood.Business.Models.Stock;

public class StockItem
{
    public long Id { get; private set; }

    public long ProductId { get; private set; }

    public Product? Product { get; private set; }

    public int Quantity { get; private set; }

    public DateTime? ExpirationDate { get; private set; }

    public DateTime CreatedAt { get; private set; }

    private readonly List<StockMovement> _movements = new();

    public IReadOnlyCollection<StockMovement> Movements => _movements;

    protected StockItem() { }

    public StockItem(long productId, int initialQuantity, DateTime? expirationDate = null)
    {
        ProductId = productId;
        CreatedAt = DateTime.UtcNow;
        SetExpirationDate(expirationDate);
        RegisterEntry(initialQuantity, "Estoque inicial");
    }

    public void RegisterEntry(int quantity, string reason = "Entrada")
    {
        if (quantity <= 0)
            throw new ArgumentException("A quantidade adicionada deve ser maior que zero.", nameof(quantity));

        Quantity += quantity;
        _movements.Add(new StockMovement(StockMovementType.Entry, quantity, reason));
    }

    public void RegisterExit(int quantity, string reason = "Saída")
    {
        if (quantity <= 0)
            throw new ArgumentException("A quantidade removida deve ser maior que zero.", nameof(quantity));

        if (Quantity < quantity)
            throw new InvalidOperationException("Quantidade em estoque insuficiente.");

        Quantity -= quantity;
        _movements.Add(new StockMovement(StockMovementType.Exit, quantity, reason));
    }

    private void SetExpirationDate(DateTime? expirationDate)
    {
        if (expirationDate.HasValue && expirationDate.Value <= DateTime.UtcNow)
            throw new ArgumentException("Data de validade deve ser futura.");

        ExpirationDate = expirationDate;
    }

}
