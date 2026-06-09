namespace LunarColonySimulator.Common.Exceptions;

public sealed class SensorFailureException : ColonyException
{
    public string SensorId { get; }

    public SensorFailureException(string sensorId, string reason)
        : base($"Falha no sensor '{sensorId}': {reason}")
    {
        SensorId = sensorId;
    }

    public SensorFailureException(string sensorId, string reason, Exception inner)
        : base($"Falha no sensor '{sensorId}': {reason}", inner)
    {
        SensorId = sensorId;
    }
}
