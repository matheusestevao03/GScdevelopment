using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Domain.Entities;

// Satélite em órbita lunar — atua como gateway de telemetria entre a colônia e a Terra.
public sealed class Satellite
{
    public string Id { get; }
    public string Name { get; }
    public double OrbitAltitudeKm { get; }
    public Coordinates GroundTrack { get; private set; }
    public DateTime LaunchDate { get; }
    public bool IsActive { get; private set; }

    public Satellite(string id, string name, double altitudeKm, Coordinates initialGroundTrack, DateTime launchDate)
    {
        Id = id;
        Name = name;
        OrbitAltitudeKm = altitudeKm;
        GroundTrack = initialGroundTrack;
        LaunchDate = launchDate;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;

    public void UpdateGroundTrack(Coordinates newPosition) => GroundTrack = newPosition;

    public TimeSpan TimeInOrbit() => DateTime.UtcNow - LaunchDate;

    public override string ToString() =>
        $"Sat {Id} '{Name}' @ {OrbitAltitudeKm}km — órbita há {TimeInOrbit().TotalDays:F0} dias";
}
