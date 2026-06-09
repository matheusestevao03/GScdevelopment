namespace LunarColonySimulator.Common.Constants;

// Classe estática que centraliza constantes físicas da Lua e parâmetros
// de simulação. Por ser estática, é compartilhada por toda a aplicação
// sem precisar ser instanciada.
public static class LunarConstants
{
    public const double LunarGravityMs2 = 1.62;
    public const double EarthGravityMs2 = 9.81;

    public const int LunarDayInEarthDays = 14;
    public const int LunarNightInEarthDays = 14;

    public const double SolarIrradianceWm2 = 1361.0;

    public const double WaterPerColonistLitersPerDay = 3.5;
    public const double OxygenPerColonistKgPerDay = 0.84;
    public const double FoodPerColonistKcalPerDay = 2400.0;
    public const double EnergyPerColonistKwhPerDay = 12.0;

    public const double MinimumSurvivalScore = 60.0;
    public const double CriticalResourceThreshold = 0.15;

    public const string SimulationLogFileName = "simulation_history.log";
}
