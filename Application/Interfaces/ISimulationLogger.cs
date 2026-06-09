namespace LunarColonySimulator.Application.Interfaces;

// Abstração de log usada pelo motor da simulação. Permite trocar a saída
// (console, arquivo, telemetria) sem alterar o engine — injeção de dependência.
public interface ISimulationLogger
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex = null);
}
