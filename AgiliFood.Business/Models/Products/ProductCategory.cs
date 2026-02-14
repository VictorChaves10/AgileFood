namespace AgiliFood.Business.Models.Products;

public class ProductCategory
{
    public int Id { get; private set; }

    public string Name { get; private set; }

    public ICollection<Product>? Products { get; private set; } = new List<Product>();

    protected ProductCategory() { }

    public ProductCategory(string name)
    {
        ChangeName(name);
    }

    public void ChangeName(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentException("O nome da categoria é obrigatório.", nameof(name));

        Name = name;
    }
}