using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

public sealed class NuclearReactor : EnergyModule
{
    public double FuelLevelPercent { get; private set; }

    public NuclearReactor(string id, Coordinates location, double nominalPowerKw)
        : base(id, $"Reator Nuclear {id}", location, nominalPowerKw)
    {
        FuelLevelPercent = 100.0;
    }

    public override ResourceLevel ProducePerTick(TimeSpan tickDuration)
    {
        if (!IsOperational || FuelLevelPercent <= 0) return ResourceLevel.Zero;

        double hours = tickDuration.TotalHours;
        double energy = NominalPowerKw * hours;
        FuelLevelPercent = Math.Max(0, FuelLevelPercent - hours * 0.05);
        return new ResourceLevel(energy, 0, 0, 0);
    }

    public override string Describe() =>
        $"Reator Nuclear {NominalPowerKw}kW — combustível: {FuelLevelPercent:F1}%";

    public override void PerformMaintenance()
    {
        base.PerformMaintenance();
        FuelLevelPercent = 100.0;
    }
}
