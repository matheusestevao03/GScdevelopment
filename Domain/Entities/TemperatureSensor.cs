namespace LunarColonySimulator.Domain.Entities;

public sealed class TemperatureSensor : Sensor
{
    public double NominalCelsius { get; }

    public TemperatureSensor(string id, double nominalCelsius) : base(id, "temperatura")
    {
        NominalCelsius = nominalCelsius;
    }

    public override string Unit => "°C";

    protected override double MeasureValue() =>
        NominalCelsius + (Rng.NextDouble() - 0.5) * 8.0;

    protected override bool IsAnomalous(double value) =>
        value < NominalCelsius - 10 || value > NominalCelsius + 10;
}
