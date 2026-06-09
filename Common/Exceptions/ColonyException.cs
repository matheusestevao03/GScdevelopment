namespace LunarColonySimulator.Common.Exceptions;

// Exceção base do domínio. Todas as exceções de negócio herdam dela,
// permitindo capturar erros específicos do simulador sem prender
// exceções gerais do .NET.
public class ColonyException : Exception
{
    public DateTime OccurredAt { get; }

    public ColonyException(string message) : base(message)
    {
        OccurredAt = DateTime.UtcNow;
    }

    public ColonyException(string message, Exception inner) : base(message, inner)
    {
        OccurredAt = DateTime.UtcNow;
    }
}
