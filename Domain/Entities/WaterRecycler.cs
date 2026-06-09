using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

public sealed class WaterRecycler : Module, IResourceProducer, IResourceConsumer
{
    public double LitersPerHour { get; }
    public double EnergyDrawKw { get; }

    public WaterRecycler(string id, Coordinates location, double litersPerHour, double energyDrawKw)
        : base(id, $"Reciclador de Água {id}", location)
    {
        LitersPerHour = litersPerHour;
        EnergyDrawKw = energyDrawKw;
    }

    public string ProducerName => Name;
    public string ConsumerName => Name;

    public ResourceLevel ProducePerTick(TimeSpan tickDuration)
    {
        if (!IsOperational) return ResourceLevel.Zero;
        return new ResourceLevel(0, LitersPerHour * tickDuration.TotalHours, 0, 0);
    }

    public ResourceLevel ConsumePerTick(TimeSpan tickDuration)
    {
        if (!IsOperational) return ResourceLevel.Zero;
        return new ResourceLevel(EnergyDrawKw * tickDuration.TotalHours, 0, 0, 0);
    }

    public override string Describe() =>
        $"Reciclador {LitersPerHour}L/h — consome {EnergyDrawKw}kW";
}
