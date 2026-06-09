using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Common.Constants;
using LunarColonySimulator.Common.Exceptions;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

// Segunda metade da partial class Colony — concentra a lógica de
// simulação (produção, consumo, manutenção).
public partial class Colony
{
    // Retorna o saldo líquido de recursos após um tick. Lança exceção
    // específica se algum recurso esgotar — críticos espaciais não podem
    // continuar como se nada tivesse acontecido.
    public ResourceLevel RunTick(TimeSpan tickDuration)
    {
        if (tickDuration <= TimeSpan.Zero)
            throw new InvalidColonyConfigurationException(nameof(tickDuration), "Tick deve ser positivo.");

        ResourceLevel produced = ResourceLevel.Zero;
        ResourceLevel consumed = ConsumePopulation(tickDuration);

        foreach (var module in _modules)
        {
            if (module is IResourceProducer producer)
                produced += producer.ProducePerTick(tickDuration);
            if (module is IResourceConsumer consumer)
                consumed += consumer.ConsumePerTick(tickDuration);
        }

        Resources += produced;
        Resources -= consumed;

        ClampAndAlertIfDepleted();
        return produced - consumed;
    }

    private ResourceLevel ConsumePopulation(TimeSpan tickDuration)
    {
        double days = tickDuration.TotalDays;
        int pop = PopulationCount;
        return new ResourceLevel(
            LunarConstants.EnergyPerColonistKwhPerDay * pop * days,
            LunarConstants.WaterPerColonistLitersPerDay * pop * days,
            LunarConstants.OxygenPerColonistKgPerDay * pop * days,
            LunarConstants.FoodPerColonistKcalPerDay * pop * days);
    }

    private void ClampAndAlertIfDepleted()
    {
        if (Resources.EnergyKwh < 0)
            throw new ResourceDepletionException("Energia", Resources.EnergyKwh);
        if (Resources.WaterLiters < 0)
            throw new ResourceDepletionException("Água", Resources.WaterLiters);
        if (Resources.OxygenKg < 0)
            throw new ResourceDepletionException("Oxigênio", Resources.OxygenKg);
        if (Resources.FoodKcal < 0)
            throw new ResourceDepletionException("Alimento", Resources.FoodKcal);
    }
}
