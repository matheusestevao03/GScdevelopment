using LunarColonySimulator.Application.Interfaces;

namespace LunarColonySimulator.Application.Services;

// Container de DI minimalista, suficiente para um Console App.
// Centraliza o "wiring" das interfaces para implementações concretas,
// mantendo o Program.cs limpo.
public static class DependencyContainer
{
    public static SimulationEngine BuildEngine(bool persistLogToFile = true)
    {
        ISimulationLogger logger = new ConsoleLogger();
        if (persistLogToFile)
            logger = new FileLogger(logger);

        IAlertService alerts = new AlertService(logger);
        ISurvivalCalculator calculator = new AISurvivalCalculator();

        return new SimulationEngine(logger, calculator, alerts);
    }
}
