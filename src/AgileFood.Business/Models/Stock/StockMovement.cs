using AgileFood.Business.Models.Consumptions;

namespace AgileFood.Business.Models.Stock;

public class StockMovement
{
    public long Id { get; private set; }

    public long StockItemId { get; private set; }

    public StockMovementType Type { get; private set; }

    public StockMovementOrigin Origin { get; private set; }

    public int Quantity { get; private set; }

    public string? Reason { get; private set; }

    public long? ConsumptionId { get; private set; }

    public Consumption? Consumption { get; private set; }

    public DateTime Date { get; private set; }

    protected StockMovement() { }

    internal StockMovement(StockMovementType type, StockMovementOrigin origin, int quantity, string reason, long? consumptionId = null)
    {
        Type = type;
        Origin = origin;
        Quantity = quantity;
        Reason = reason;
        ConsumptionId = consumptionId;
        Date = DateTime.UtcNow;
    }
}
