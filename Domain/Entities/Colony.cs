using LunarColonySimulator.Common.Exceptions;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

// Classe PARTIAL — dividida em dois arquivos para separar responsabilidades:
//   Colony.cs            -> estado, propriedades e configuração
//   Colony.Simulation.cs -> lógica de ticks e cálculo
public partial class Colony
{
    public string Name { get; }
    public Coordinates Location { get; }
    public DateTime FoundedAt { get; }
    public ResourceLevel Resources { get; private set; }

    private readonly List<Module> _modules = new();
    private readonly List<Sensor> _sensors = new();
    private readonly List<Colonist> _colonists = new();
    private readonly List<Satellite> _satellites = new();

    public IReadOnlyList<Module> Modules => _modules;
    public IReadOnlyList<Sensor> Sensors => _sensors;
    public IReadOnlyList<Colonist> Colonists => _colonists;
    public IReadOnlyList<Satellite> Satellites => _satellites;

    public Colony(string name, Coordinates location, ResourceLevel initialStock)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidColonyConfigurationException(nameof(name), "Nome da colônia obrigatório.");

        Name = name;
        Location = location;
        FoundedAt = DateTime.UtcNow;
        Resources = initialStock;
    }

    public void AddModule(Module module) => _modules.Add(module);
    public void AddSensor(Sensor sensor) => _sensors.Add(sensor);
    public void AddColonist(Colonist colonist) => _colonists.Add(colonist);
    public void AddSatellite(Satellite satellite) => _satellites.Add(satellite);

    public int PopulationCount => _colonists.Count;

    public double AverageHealth() =>
        _colonists.Count == 0 ? 0 : _colonists.Average(c => c.HealthPercent);

    public TimeSpan Age() => DateTime.UtcNow - FoundedAt;
}
