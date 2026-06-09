namespace LunarColonySimulator.Common.Exceptions;

public sealed class ResourceDepletionException : ColonyException
{
    public string ResourceName { get; }
    public double RemainingAmount { get; }

    public ResourceDepletionException(string resourceName, double remaining)
        : base($"Recurso '{resourceName}' esgotado. Restante: {remaining:F2}.")
    {
        ResourceName = resourceName;
        RemainingAmount = remaining;
    }
}
