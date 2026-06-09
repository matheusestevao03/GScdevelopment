namespace LunarColonySimulator.Domain.Entities;

public sealed class OxygenSensor : Sensor
{
    public OxygenSensor(string id) : base(id, "oxigênio") { }

    public override string Unit => "%";

    protected override double MeasureValue() => 19.0 + Rng.NextDouble() * 4.0;

    protected override bool IsAnomalous(double value) => value < 18.0 || value > 23.5;
}
