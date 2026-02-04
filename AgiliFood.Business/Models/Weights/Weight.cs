namespace AgiliFood.Business.Models.Weights;

public class Weight
{
    public long Id { get; private set; }

    public double Amount { get; private set; }

    public WeightUnitEnum WeightUnit { get; private set; }

    protected Weight() { }

    public Weight(double amount, WeightUnitEnum weightUnit)
    {
        SetAmount(amount);
        SetWeightUnit(weightUnit);
    }

    public void SetAmount(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(amount));

        Amount = amount;
    }

    public void SetWeightUnit(WeightUnitEnum weightUnit)
    {
        if (!Enum.IsDefined(typeof(WeightUnitEnum), weightUnit))
            throw new ArgumentException("Unidade de peso inválida.", nameof(weightUnit));

        WeightUnit = weightUnit;
    }
}
