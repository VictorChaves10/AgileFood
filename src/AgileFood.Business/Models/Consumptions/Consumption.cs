using AgileFood.Business.Models.Users;

namespace AgileFood.Business.Models.Consumptions;

public class Consumption
{
    public long Id { get; private set; }

    public long UserId { get; private set; }

    public User? User { get; private set; }

    public decimal TotalPrice { get; private set; }

    public DateTime ConsumedAt { get; private set; }

    public int ReferenceMonth { get; private set; }

    public int ReferenceYear { get; private set; }

    private readonly List<ConsumptionItem> _items = new();

    public IReadOnlyCollection<ConsumptionItem> Items => _items.AsReadOnly();

    protected Consumption() { }

    public Consumption(long userId)
    {
        if (userId <= 0)
            throw new ArgumentException("O usuario e obrigatorio.", nameof(userId));

        UserId = userId;
        ConsumedAt = DateTime.UtcNow;
        ReferenceMonth = ConsumedAt.Month;
        ReferenceYear = ConsumedAt.Year;
    }

    public void AddItem(long productId, string productName, decimal unitPrice, int quantity)
    {
        var item = new ConsumptionItem(productId, productName, unitPrice, quantity);

        _items.Add(item);
        TotalPrice += item.TotalPrice;
    }

    public int GetTotalItems()
    {
        return _items.Sum(i => i.Quantity);
    }
}
