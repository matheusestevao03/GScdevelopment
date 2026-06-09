using LunarColonySimulator.Application.Interfaces;
using LunarColonySimulator.Common.Constants;
using LunarColonySimulator.Domain.Entities;

namespace LunarColonySimulator.Application.Services;

// O "Core IA" do simulador — heurística que pondera recursos, módulos,
// população e saúde para gerar um score de sobrevivência (0-100) e
// recomendações textuais. Não usa ML real, mas oferece a interface
// estável que o frontend ou um clipper externo poderia consumir.
public sealed class AISurvivalCalculator : ISurvivalCalculator
{
    public double CalculateSurvivalScore(Colony colony)
    {
        if (colony.PopulationCount == 0) return 0;

        double daysOfEnergy = colony.Resources.EnergyKwh /
            Math.Max(1, colony.PopulationCount * LunarConstants.EnergyPerColonistKwhPerDay);
        double daysOfWater = colony.Resources.WaterLiters /
            Math.Max(1, colony.PopulationCount * LunarConstants.WaterPerColonistLitersPerDay);
        double daysOfOxygen = colony.Resources.OxygenKg /
            Math.Max(1, colony.PopulationCount * LunarConstants.OxygenPerColonistKgPerDay);
        double daysOfFood = colony.Resources.FoodKcal /
            Math.Max(1, colony.PopulationCount * LunarConstants.FoodPerColonistKcalPerDay);

        double autonomy = Math.Min(Math.Min(daysOfEnergy, daysOfWater),
                                   Math.Min(daysOfOxygen, daysOfFood));

        double autonomyScore = Math.Clamp(autonomy / 30.0 * 60.0, 0, 60);
        double healthScore = colony.AverageHealth() * 0.3;
        double redundancyScore = Math.Min(colony.Modules.Count, 10);

        return Math.Round(autonomyScore + healthScore + redundancyScore, 2);
    }

    public string GenerateRecommendation(Colony colony)
    {
        double score = CalculateSurvivalScore(colony);
        var sb = new System.Text.StringBuilder();

        sb.AppendLine($"Score de Sobrevivência: {score}/100");

        if (score >= LunarConstants.MinimumSurvivalScore)
            sb.AppendLine(">> Colônia estável. Pode-se planejar expansão.");
        else
            sb.AppendLine(">> Colônia em risco. Reforce os pontos abaixo:");

        if (colony.Resources.EnergyKwh < colony.PopulationCount * LunarConstants.EnergyPerColonistKwhPerDay * 5)
            sb.AppendLine("   - Aumentar geração de energia (painéis solares ou reator).");
        if (colony.Resources.WaterLiters < colony.PopulationCount * LunarConstants.WaterPerColonistLitersPerDay * 5)
            sb.AppendLine("   - Implantar ou expandir reciclador de água.");
        if (colony.Resources.FoodKcal < colony.PopulationCount * LunarConstants.FoodPerColonistKcalPerDay * 5)
            sb.AppendLine("   - Aumentar área de estufa para produção alimentar.");
        if (colony.Sensors.Count < 3)
            sb.AppendLine("   - Aumentar redundância de sensores ambientais.");

        return sb.ToString();
    }
}
