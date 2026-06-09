using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Domain.Entities;

namespace LunarColonySimulator.Application.Services;

public sealed class AlertService : IAlertService
{
    private readonly List<Alert> _history = new();
    private readonly ISimulationLogger _logger;

    public AlertService(ISimulationLogger logger)
    {
        _logger = logger;
    }

    public IReadOnlyList<Alert> History => _history;

    public void Raise(Alert alert)
    {
        _history.Add(alert);
        switch (alert.Severity)
        {
            case AlertSeverity.Info:
                _logger.LogInfo(alert.ToString());
                break;
            case AlertSeverity.Warning:
                _logger.LogWarning(alert.ToString());
                break;
            case AlertSeverity.Critical:
                _logger.LogError(alert.ToString());
                break;
        }
    }
}
