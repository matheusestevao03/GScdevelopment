namespace LunarColonySimulator.Domain.Entities;

public sealed class PressureSensor : Sensor
{
    public PressureSensor(string id) : base(id, "pressão") { }

    public override string Unit => "kPa";

    protected override double MeasureValue() => 99.0 + Rng.NextDouble() * 4.0;

    protected override bool IsAnomalous(double value) => value < 95.0 || value > 105.0;
}
