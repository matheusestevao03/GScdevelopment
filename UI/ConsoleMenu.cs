using LunarColonySimulator.Common.Exceptions;
using LunarColonySimulator.Domain.Entities;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.UI;

// Camada de apresentação em modo console. Encapsula a leitura de entradas
// e a construção da colônia conforme o usuário escolhe.
public static class ConsoleMenu
{
    public static void PrintBanner()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║         LUNAR COLONY SIMULATOR — Core IA v1.0          ║");
        Console.WriteLine("║      FIAP Global Solution — Digital Twin Lunar         ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    public static (Colony colony, int days, TimeSpan tick) PromptScenario()
    {
        Console.WriteLine();
        Console.WriteLine("Escolha o cenário:");
        Console.WriteLine("  [1] Colônia Demo (Mare Tranquillitatis, 6 colonos)");
        Console.WriteLine("  [2] Colônia Customizada (você define os parâmetros)");
        Console.Write("> ");
        string? choice = Console.ReadLine();

        Colony colony = choice == "2" ? BuildCustomColony() : BuildDemoColony();
        int days = ReadInt("Quantos dias terrestres simular? (padrão 14) > ", 14);
        TimeSpan tick = TimeSpan.FromHours(ReadInt("Duração de cada tick (horas, padrão 6) > ", 6));
        return (colony, days, tick);
    }

    private static Colony BuildDemoColony()
    {
        var location = new Coordinates(0.67, 23.47);
        var initial = new ResourceLevel(2400, 1200, 400, 96000);
        var colony = new Colony("Tranquility Base", location, initial);

        colony.AddModule(new SolarPanel("SP-01", location, nominalPowerKw: 25, efficiencyPercent: 22));
        colony.AddModule(new SolarPanel("SP-02", location, 25, 22));
        colony.AddModule(new NuclearReactor("NR-01", location, nominalPowerKw: 40));
        colony.AddModule(new WaterRecycler("WR-01", location, litersPerHour: 4, energyDrawKw: 1.2));
        colony.AddModule(new Greenhouse("GH-01", location, kcalPerHour: 800, oxygenKgPerHour: 0.05,
            waterConsumption: 2, energyDrawKw: 3));
        colony.AddModule(new HabitatDome("HD-01", location, capacity: 8, energyDrawKw: 2));

        colony.AddSensor(new TemperatureSensor("TEMP-01", nominalCelsius: 21));
        colony.AddSensor(new OxygenSensor("O2-01"));
        colony.AddSensor(new PressureSensor("PRESS-01"));

        DateTime arrival = DateTime.UtcNow.AddDays(-30);
        colony.AddColonist(new Colonist("C-001", "Ana Souza", "Comandante", arrival));
        colony.AddColonist(new Colonist("C-002", "Bruno Lima", "Engenheiro de Energia", arrival));
        colony.AddColonist(new Colonist("C-003", "Camila Reis", "Biólogo", arrival));
        colony.AddColonist(new Colonist("C-004", "Diego Alves", "Médico", arrival.AddDays(7)));
        colony.AddColonist(new Colonist("C-005", "Erika Tan", "Geóloga", arrival.AddDays(7)));
        colony.AddColonist(new Colonist("C-006", "Felipe Cruz", "Especialista TI", arrival.AddDays(14)));

        colony.AddSatellite(new Satellite("LCS-RELAY-1", "Selene Relay",
            altitudeKm: 100, location, launchDate: DateTime.UtcNow.AddYears(-1)));

        return colony;
    }

    private static Colony BuildCustomColony()
    {
        string name = ReadString("Nome da colônia: ");
        double lat = ReadDouble("Latitude selenográfica (-90 a 90): ", 0);
        double lon = ReadDouble("Longitude selenográfica (-180 a 180): ", 0);
        int pop = ReadInt("Número inicial de colonos: ", 4);
        int panels = ReadInt("Quantos painéis solares (25 kW cada)? ", 2);
        int greenhouses = ReadInt("Quantas estufas? ", 1);

        var location = new Coordinates(lat, lon);
        var initial = new ResourceLevel(pop * 200, pop * 100, pop * 30, pop * 8000);

        var colony = new Colony(name, location, initial);

        for (int i = 1; i <= panels; i++)
            colony.AddModule(new SolarPanel($"SP-{i:00}", location, 25, 22));
        for (int i = 1; i <= greenhouses; i++)
            colony.AddModule(new Greenhouse($"GH-{i:00}", location, 800, 0.05, 2, 3));

        colony.AddModule(new WaterRecycler("WR-01", location, 4, 1.2));
        colony.AddModule(new HabitatDome("HD-01", location, pop + 2, 2));

        colony.AddSensor(new TemperatureSensor("TEMP-01", 21));
        colony.AddSensor(new OxygenSensor("O2-01"));
        colony.AddSensor(new PressureSensor("PRESS-01"));

        DateTime arrival = DateTime.UtcNow;
        for (int i = 1; i <= pop; i++)
            colony.AddColonist(new Colonist($"C-{i:000}", $"Colono {i}", "Generalista", arrival));

        colony.AddSatellite(new Satellite("LCS-RELAY-1", "Relay Custom", 100, location, DateTime.UtcNow.AddMonths(-3)));
        return colony;
    }

    private static string ReadString(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            throw new InvalidColonyConfigurationException("input", "Entrada vazia.");
        return input.Trim();
    }

    private static int ReadInt(string prompt, int defaultValue)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        return int.TryParse(input, out int value) ? value : defaultValue;
    }

    private static double ReadDouble(string prompt, double defaultValue)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        return double.TryParse(input, System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture, out double v) ? v : defaultValue;
    }
}
