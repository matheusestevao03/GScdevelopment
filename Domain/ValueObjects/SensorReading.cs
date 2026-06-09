namespace LunarColonySimulator.Domain.ValueObjects;

// Struct imutável de uma leitura de sensor, carimbada com data/hora UTC.
public readonly struct SensorReading
{
    public string SensorId { get; }
    public double Value { get; }
    public string Unit { get; }
    public DateTime TimestampUtc { get; }
    public bool IsAnomalous { get; }

    public SensorReading(string sensorId, double value, string unit, bool isAnomalous)
    {
        SensorId = sensorId;
        Value = value;
        Unit = unit;
        TimestampUtc = DateTime.UtcNow;
        IsAnomalous = isAnomalous;
    }

    public override string ToString() =>
        $"[{TimestampUtc:HH:mm:ss}] {SensorId}: {Value:F2} {Unit}{(IsAnomalous ? " ⚠" : string.Empty)}";
}
