using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

// HERANÇA: SolarPanel é um EnergyModule especializado.
// POLIMORFISMO: sobrescreve ProducePerTick e Describe.
public sealed class SolarPanel : EnergyModule
{
    public double EfficiencyPercent { get; }
    public bool IsLunarDay { get; set; }

    public SolarPanel(string id, Coordinates location, double nominalPowerKw, double efficiencyPercent)
        : base(id, $"Painel Solar {id}", location, nominalPowerKw)
    {
        EfficiencyPercent = efficiencyPercent;
        IsLunarDay = true;
    }

    public override ResourceLevel ProducePerTick(TimeSpan tickDuration)
    {
        if (!IsOperational || !IsLunarDay) return ResourceLevel.Zero;

        double hours = tickDuration.TotalHours;
        double energy = NominalPowerKw * (EfficiencyPercent / 100.0) * hours;
        return new ResourceLevel(energy, 0, 0, 0);
    }

    public override string Describe() =>
        $"Painel Solar {NominalPowerKw}kW @ {EfficiencyPercent}% eficiência em {Location}";
}
