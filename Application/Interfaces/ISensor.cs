using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Application.Interfaces;

public interface ISensor
{
    string Id { get; }
    string MeasuredQuantity { get; }
    SensorReading Read();
}
