using AgileFood.Business.Models.Products;

namespace AgileFood.Business.Models.Catalogs;

public class CatalogItem
{
    public long Id { get; private set; }

    public long ProductId { get; private set; }

    public Product? Product { get; private set; }

    public bool IsAvailable { get; private set; }

    public DateTime CreatedAt { get; private set; }

    protected CatalogItem() { }

    public CatalogItem(long productId)
    {
        SetProduct(productId);
        IsAvailable = true;
        CreatedAt = DateTime.UtcNow;
    }

    private void SetProduct(long productId)
    {
        if (productId <= 0)
            throw new ArgumentException("O produto é obrigatório.", nameof(productId));

        ProductId = productId;
    }

    public void MakeAvailable() => IsAvailable = true;

    public void MakeUnavailable() => IsAvailable = false;
}