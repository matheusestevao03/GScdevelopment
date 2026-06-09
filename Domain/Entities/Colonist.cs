namespace LunarColonySimulator.Domain.Entities;

// Sealed: não pode ser herdada. Representa um habitante da colônia.
public sealed class Colonist
{
    public string Id { get; }
    public string Name { get; }
    public string Specialty { get; }
    public DateTime ArrivalDate { get; }
    public double HealthPercent { get; private set; }

    public Colonist(string id, string name, string specialty, DateTime arrivalDate)
    {
        Id = id;
        Name = name;
        Specialty = specialty;
        ArrivalDate = arrivalDate;
        HealthPercent = 100.0;
    }

    public void AdjustHealth(double delta) =>
        HealthPercent = Math.Clamp(HealthPercent + delta, 0, 100);

    public TimeSpan TimeOnColony() => DateTime.UtcNow - ArrivalDate;

    public override string ToString() =>
        $"{Name} ({Specialty}) — saúde: {HealthPercent:F0}%";
}
