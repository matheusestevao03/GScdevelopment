using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

public sealed class HabitatDome : Module, IResourceConsumer
{
    public int Capacity { get; }
    public double EnergyDrawKw { get; }

    public HabitatDome(string id, Coordinates location, int capacity, double energyDrawKw)
        : base(id, $"Domo Habitacional {id}", location)
    {
        Capacity = capacity;
        EnergyDrawKw = energyDrawKw;
    }

    public string ConsumerName => Name;

    public ResourceLevel ConsumePerTick(TimeSpan tickDuration)
    {
        if (!IsOperational) return ResourceLevel.Zero;
        return new ResourceLevel(EnergyDrawKw * tickDuration.TotalHours, 0, 0, 0);
    }

    public override string Describe() =>
        $"Domo (capacidade {Capacity} colonos) — consome {EnergyDrawKw}kW";
}
