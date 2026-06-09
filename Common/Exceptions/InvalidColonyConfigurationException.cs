namespace LunarColonySimulator.Common.Exceptions;

public sealed class InvalidColonyConfigurationException : ColonyException
{
    public string ConfigurationField { get; }

    public InvalidColonyConfigurationException(string field, string message)
        : base($"Configuração inválida em '{field}': {message}")
    {
        ConfigurationField = field;
    }
}
