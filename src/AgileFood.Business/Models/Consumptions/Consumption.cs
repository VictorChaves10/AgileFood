using AgileFood.Business.Models.Products;
using AgileFood.Business.Models.Users;

namespace AgileFood.Business.Models.Consumptions;

public class Consumption
{
    public long Id { get; private set; }

    public long UserId { get; private set; }

    public User? User { get; private set; }

    public long ProductId { get; private set; }

    public Product? Product { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TotalPrice { get; private set; }

    public DateTime ConsumedAt { get; private set; }

    public int ReferenceMonth { get; private set; }

    public int ReferenceYear { get; private set; }

    protected Consumption() { }

    public Consumption(long userId, long productId, int quantity, decimal unitPrice)
    {
        if (userId <= 0)
            throw new ArgumentException("O usuário é obrigatório.", nameof(userId));

        if (productId <= 0)
            throw new ArgumentException("O produto é obrigatório.", nameof(productId));

        if (quantity <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantity));

        if (unitPrice <= 0)
            throw new ArgumentException("O preço unitário deve ser maior que zero.", nameof(unitPrice));

        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        TotalPrice = quantity * unitPrice;
        ConsumedAt = DateTime.UtcNow;
        ReferenceMonth = ConsumedAt.Month;
        ReferenceYear = ConsumedAt.Year;
    }
}