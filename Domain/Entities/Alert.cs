namespace LunarColonySimulator.Domain.Entities;

public enum AlertSeverity
{
    Info,
    Warning,
    Critical
}

public sealed class Alert
{
    public Guid Id { get; }
    public string Source { get; }
    public string Message { get; }
    public AlertSeverity Severity { get; }
    public DateTime RaisedAtUtc { get; }

    public Alert(string source, string message, AlertSeverity severity)
    {
        Id = Guid.NewGuid();
        Source = source;
        Message = message;
        Severity = severity;
        RaisedAtUtc = DateTime.UtcNow;
    }

    public override string ToString() =>
        $"[{RaisedAtUtc:HH:mm:ss}] {Severity.ToString().ToUpper()} — {Source}: {Message}";
}
