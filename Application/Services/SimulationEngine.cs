using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Common.Constants;
using LunarColonySimulator.Common.Exceptions;
using LunarColonySimulator.Domain.Entities;
using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Application.Services;

// Engine que conduz a simulação tick a tick. Recebe suas dependências via
// construtor — injeção de dependência manual sobre as interfaces da pasta
// Application/Interfaces. Permite trocar logger, calculadora de IA e
// serviço de alertas sem tocar nesta classe.
public sealed class SimulationEngine
{
    private readonly ISimulationLogger _logger;
    private readonly ISurvivalCalculator _calculator;
    private readonly IAlertService _alerts;

    public SimulationEngine(
        ISimulationLogger logger,
        ISurvivalCalculator calculator,
        IAlertService alerts)
    {
        _logger = logger;
        _calculator = calculator;
        _alerts = alerts;
    }

    public void Run(Colony colony, int totalDays, TimeSpan tickDuration)
    {
        _logger.LogInfo($"Iniciando simulação da colônia '{colony.Name}' em {colony.Location}.");
        _logger.LogInfo($"População: {colony.PopulationCount} | Módulos: {colony.Modules.Count} | Satélites: {colony.Satellites.Count}");

        DateTime simulationStart = DateTime.UtcNow;
        int tickCount = (int)Math.Ceiling(TimeSpan.FromDays(totalDays).TotalHours / tickDuration.TotalHours);

        for (int i = 1; i <= tickCount; i++)
        {
            try
            {
                ResourceLevel netFlow = colony.RunTick(tickDuration);
                EvaluateSensors(colony);

                if (i % 7 == 0 || i == tickCount)
                {
                    _logger.LogInfo($"Tick {i,3} | Idade: {colony.Age().TotalDays:F1}d | Saldo: {netFlow} | Recursos: {colony.Resources}");
                }
            }
            catch (ResourceDepletionException ex)
            {
                _alerts.Raise(new Alert("Engine", ex.Message, AlertSeverity.Critical));
                _logger.LogError("Simulação interrompida por colapso de recurso.", ex);
                break;
            }
            catch (SensorFailureException ex)
            {
                _alerts.Raise(new Alert("Sensor", ex.Message, AlertSeverity.Warning));
            }
            catch (ColonyException ex)
            {
                _logger.LogError("Erro de domínio na colônia.", ex);
                break;
            }
            catch (Exception ex)
            {
                // Último recurso: registra e segue. Sistemas espaciais críticos
                // nunca devem encerrar bruscamente — preferimos modo degradado.
                _logger.LogError("Erro inesperado capturado pelo engine — seguindo em modo degradado.", ex);
            }
        }

        TimeSpan elapsed = DateTime.UtcNow - simulationStart;
        _logger.LogInfo($"Simulação finalizada em {elapsed.TotalSeconds:F2}s (tempo real).");
        _logger.LogInfo(_calculator.GenerateRecommendation(colony));
    }

    private void EvaluateSensors(Colony colony)
    {
        foreach (var sensor in colony.Sensors)
        {
            try
            {
                var reading = sensor.Read();
                if (reading.IsAnomalous)
                {
                    _alerts.Raise(new Alert(sensor.Id,
                        $"Leitura anômala de {sensor.MeasuredQuantity}: {reading.Value:F2}{sensor.Unit}",
                        AlertSeverity.Warning));
                }
            }
            catch (SensorFailureException ex)
            {
                _alerts.Raise(new Alert(sensor.Id, ex.Message, AlertSeverity.Critical));
            }
        }
    }
}
