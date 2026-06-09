using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

// Abstração intermediária para módulos de energia. Cada filho (Solar, Nuclear)
// produz energia de forma diferente — polimorfismo em ação.
public abstract class EnergyModule : Module, IResourceProducer
{
    public double NominalPowerKw { get; }

    protected EnergyModule(string id, string name, Coordinates location, double nominalPowerKw)
        : base(id, name, location)
    {
        NominalPowerKw = nominalPowerKw;
    }

    public string ProducerName => Name;

    public abstract ResourceLevel ProducePerTick(TimeSpan tickDuration);
}
