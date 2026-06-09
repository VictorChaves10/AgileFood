namespace AgileFood.Business.Models.Consumptions;

public class ConsumptionItem
{
    public long Id { get; private set; }

    public long ConsumptionId { get; private set; }

    public long ProductId { get; private set; }

    public string ProductName { get; private set; } = null!;

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalPrice { get; private set; }

    public Consumption? Consumption { get; private set; }

    protected ConsumptionItem() { }

    public ConsumptionItem(long productId, string productName, decimal unitPrice, int quantity)
    {
        if (productId <= 0)
            throw new ArgumentException("O produto e obrigatorio.", nameof(productId));

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("O nome do produto e obrigatorio.", nameof(productName));

        if (unitPrice <= 0)
            throw new ArgumentException("O preco unitario deve ser maior que zero.", nameof(unitPrice));

        if (quantity <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantity));

        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        TotalPrice = unitPrice * quantity;
    }
}
