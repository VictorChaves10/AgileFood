using AgiliFood.Business.Models.Weights;

namespace AgiliFood.Business.Models.Products;

public class Product
{
    public long Id { get; private set; }

    public string? Name { get; private set; }

    public string? Description { get; private set; }

    public string? Brand { get; private set; }

    public string? Flavor { get; private set; }

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; }

    public string? BarCode { get; private set; }

    public string? Image { get; private set; }

    public long IdWeight { get; private set; }

    public int ProductCategoryId { get; private set; }

    public Weight Weight { get; private set; }

    public ProductCategory? ProductCategory { get; private set; }

    protected Product() { } 

    public Product(string name, string? description, string? brand, string flavor,
                   decimal price, bool isActive, string? barCode,  string? image,
                   int productCategoryId, Weight weight)
    {
        SetName(name);
        SetFlavor(flavor);
        SetWeight(weight);
        ChangePrice(price);

        Description = description;
        Brand = brand;
        IsActive = isActive;
        BarCode = barCode;
        Image = image;
        ProductCategoryId = productCategoryId;
    }


    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(name));

        Name = name;
    }

    public void SetFlavor(string flavor)
    {
        Flavor = flavor;
    }

    public void SetWeight(Weight weight)
    {
        if (weight == null)
            throw new ArgumentNullException(nameof(weight), "O peso do produto é obrigatório.");

        if (weight.Amount <= 0)
            throw new ArgumentException("O peso do produto deve ser maior que zero.", nameof(weight));

        Weight = weight;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("Preço não pode ser negativo.", nameof(newPrice));

        Price = newPrice;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    public void ChangeCategory(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("O id da categoria não pode ser nulo.", nameof(categoryId));

        ProductCategoryId = categoryId;
    }

    public void ChangeImage(string? image)
    {
        Image = image;
    }

}
