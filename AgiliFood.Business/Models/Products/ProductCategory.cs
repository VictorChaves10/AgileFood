namespace AgiliFood.Business.Models.Products;

public class ProductCategory
{
    public int Id { get; private set; }

    public string? Name { get; private set; }

    public ICollection<Product>? Products { get; private set; }

    protected ProductCategory() { }

    public ProductCategory(string name)
    {
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}