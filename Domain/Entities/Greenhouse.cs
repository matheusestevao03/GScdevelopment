using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

public sealed class Greenhouse : Module, IResourceProducer, IResourceConsumer
{
    public double KcalPerHour { get; }
    public double OxygenKgPerHour { get; }
    public double WaterConsumptionLitersPerHour { get; }
    public double EnergyDrawKw { get; }

    public Greenhouse(string id, Coordinates location, double kcalPerHour, double oxygenKgPerHour,
        double waterConsumption, double energyDrawKw)
        : base(id, $"Estufa {id}", location)
    {
        KcalPerHour = kcalPerHour;
        OxygenKgPerHour = oxygenKgPerHour;
        WaterConsumptionLitersPerHour = waterConsumption;
        EnergyDrawKw = energyDrawKw;
    }

    public string ProducerName => Name;
    public string ConsumerName => Name;

    public ResourceLevel ProducePerTick(TimeSpan tickDuration)
    {
        if (!IsOperational) return ResourceLevel.Zero;
        double h = tickDuration.TotalHours;
        return new ResourceLevel(0, 0, OxygenKgPerHour * h, KcalPerHour * h);
    }

    public ResourceLevel ConsumePerTick(TimeSpan tickDuration)
    {
        if (!IsOperational) return ResourceLevel.Zero;
        double h = tickDuration.TotalHours;
        return new ResourceLevel(EnergyDrawKw * h, WaterConsumptionLitersPerHour * h, 0, 0);
    }

    public override string Describe() =>
        $"Estufa {KcalPerHour}kcal/h, O2 {OxygenKgPerHour}kg/h, água {WaterConsumptionLitersPerHour}L/h";
}
