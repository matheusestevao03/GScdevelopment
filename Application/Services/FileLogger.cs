using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Common.Constants;

namespace LunarColonySimulator.Application.Services;

// Decorator simples — escreve no console e também persiste em arquivo,
// gerando histórico com DateTime preciso para auditoria.
public sealed class FileLogger : ISimulationLogger
{
    private readonly ISimulationLogger _inner;
    private readonly string _path;

    public FileLogger(ISimulationLogger inner, string? path = null)
    {
        _inner = inner;
        _path = path ?? LunarConstants.SimulationLogFileName;
    }

    public void LogInfo(string message)
    {
        _inner.LogInfo(message);
        Append("INFO", message);
    }

    public void LogWarning(string message)
    {
        _inner.LogWarning(message);
        Append("WARN", message);
    }

    public void LogError(string message, Exception? ex = null)
    {
        _inner.LogError(message, ex);
        Append("ERRO", ex is null ? message : $"{message} :: {ex.GetType().Name} - {ex.Message}");
    }

    private void Append(string level, string message)
    {
        try
        {
            File.AppendAllText(_path, $"[{DateTime.UtcNow:O}] {level} {message}{Environment.NewLine}");
        }
        catch (IOException)
        {
            // Falha de escrita não deve derrubar a simulação — apenas seguimos.
        }
    }
}
