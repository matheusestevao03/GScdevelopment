namespace LunarColonySimulator.Domain.ValueObjects;

// Struct que representa o estoque de recursos vitais da colônia.
// Operadores sobrecarregados facilitam o consumo/produção a cada tick da simulação.
public struct ResourceLevel
{
    public double EnergyKwh { get; set; }
    public double WaterLiters { get; set; }
    public double OxygenKg { get; set; }
    public double FoodKcal { get; set; }

    public ResourceLevel(double energy, double water, double oxygen, double food)
    {
        EnergyKwh = energy;
        WaterLiters = water;
        OxygenKg = oxygen;
        FoodKcal = food;
    }

    public static ResourceLevel Zero => new(0, 0, 0, 0);

    public static ResourceLevel operator +(ResourceLevel a, ResourceLevel b) =>
        new(a.EnergyKwh + b.EnergyKwh, a.WaterLiters + b.WaterLiters,
            a.OxygenKg + b.OxygenKg, a.FoodKcal + b.FoodKcal);

    public static ResourceLevel operator -(ResourceLevel a, ResourceLevel b) =>
        new(a.EnergyKwh - b.EnergyKwh, a.WaterLiters - b.WaterLiters,
            a.OxygenKg - b.OxygenKg, a.FoodKcal - b.FoodKcal);

    public bool HasAnyDepleted() =>
        EnergyKwh <= 0 || WaterLiters <= 0 || OxygenKg <= 0 || FoodKcal <= 0;

    public override string ToString() =>
        $"[E:{EnergyKwh:F1}kWh | A:{WaterLiters:F1}L | O2:{OxygenKg:F1}kg | F:{FoodKcal:F0}kcal]";
}
