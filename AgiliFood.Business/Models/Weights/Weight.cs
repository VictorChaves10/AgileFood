namespace AgiliFood.Business.Models.Weights;

public class Weight
{
    public double Amount { get; }

    public WeightUnitEnum Unit { get; }

    protected Weight() { }

    public Weight(double amount, WeightUnitEnum unit)
    {
        if (amount <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.");

        if (!Enum.IsDefined(typeof(WeightUnitEnum), unit))
            throw new ArgumentException("Unidade de peso inválida.");

        Amount = amount;
        Unit = unit;
    }
}

