using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Common.Exceptions;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

// Classe abstrata para sensores. Define o template Read() que valida a leitura,
// delegando a geração do valor para o método abstrato MeasureValue().
public abstract class Sensor : ISensor
{
    public string Id { get; }
    public string MeasuredQuantity { get; }
    public DateTime InstalledAt { get; }

    // Static — gerador de números aleatórios compartilhado entre sensores.
    protected static readonly Random Rng = new();

    protected Sensor(string id, string measuredQuantity)
    {
        Id = id;
        MeasuredQuantity = measuredQuantity;
        InstalledAt = DateTime.UtcNow;
    }

    public SensorReading Read()
    {
        try
        {
            double value = MeasureValue();
            bool anomaly = IsAnomalous(value);
            return new SensorReading(Id, value, Unit, anomaly);
        }
        catch (Exception ex) when (ex is not SensorFailureException)
        {
            throw new SensorFailureException(Id, "erro inesperado ao medir", ex);
        }
    }

    protected abstract double MeasureValue();
    protected abstract bool IsAnomalous(double value);
    public abstract string Unit { get; }
}
