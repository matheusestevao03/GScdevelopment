using LunarColonySimulator.Application.Interfaces;

namespace LunarColonySimulator.Application.Services;

public sealed class ConsoleLogger : ISimulationLogger
{
    public void LogInfo(string message) => Write("INFO", ConsoleColor.Cyan, message);
    public void LogWarning(string message) => Write("WARN", ConsoleColor.Yellow, message);

    public void LogError(string message, Exception? ex = null)
    {
        Write("ERRO", ConsoleColor.Red, ex is null ? message : $"{message} :: {ex.GetType().Name} - {ex.Message}");
    }

    private static void Write(string level, ConsoleColor color, string message)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine($"[{DateTime.UtcNow:HH:mm:ss}] {level,-4} {message}");
        Console.ForegroundColor = prev;
    }
}
