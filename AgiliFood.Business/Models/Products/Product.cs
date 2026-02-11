using AgiliFood.Business.Models.Weights;

namespace AgiliFood.Business.Models.Products;

public class Product
{
    public long Id { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public string? Brand { get; private set; }

    public string? Flavor { get; private set; }

    public decimal Price { get; private set; }

    public bool IsActive { get; private set; }

    public string? BarCode { get; private set; }

    public string? Image { get; private set; }

    public int ProductCategoryId { get; private set; }

    public Weight Weight { get; private set; }

    public ProductCategory? ProductCategory { get; private set; }

    protected Product() { }

    public Product(string name, string? description, string? brand, string? flavor,
                   decimal price, bool isActive, string? barCode, string? image,
                   int productCategoryId, decimal weightAmount, WeightUnitEnum weightUnit)
    {
        ChangeName(name);
        ChangeWeight(weightAmount, weightUnit);
        ChangePrice(price);

        Description = description;
        Brand = brand;
        IsActive = isActive;
        BarCode = barCode;
        Image = image;
        ProductCategoryId = productCategoryId;
        Flavor = flavor;
    }

    public void Update(
        string name,
        string? flavor,
        string? brand,
        string? barcode,
        decimal price,
        int categoryId,
        decimal weightAmount,
        WeightUnitEnum weightUnit,
        string? image,
        bool isActive)
    {
        if (Name != name) ChangeName(name);
        if (Price != price) ChangePrice(price);
        if (Image != image) ChangeImage(image);
        if (Flavor != flavor) ChangeFlavor(flavor);
        if (Brand != brand) ChangeBrand(brand);
        if (BarCode != barcode) ChangeBarCode(barcode);
        if (ProductCategoryId != categoryId) ChangeCategory(categoryId);

        if (Weight.Amount != weightAmount || Weight.Unit != weightUnit)
            ChangeWeight(weightAmount, weightUnit);

        if (isActive)
            Activate();
        else
            Deactivate();
    }



    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(name));

        Name = name;
    }

    public void ChangeWeight(decimal amount, WeightUnitEnum unit)
    {
        Weight = new Weight(amount, unit);
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice <= 0)
            throw new ArgumentException("O preço deve ser maior que zero.", nameof(newPrice));

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

    public void ChangeFlavor(string? flavor)
    {
        Flavor = flavor;
    }

    public void ChangeBrand(string? brand)
    {
        Brand = brand;
    }

    public void ChangeBarCode(string? barCode)
    {
        BarCode = barCode;
    }

}
