using LunarColonySimulator.Domain.Entities;

namespace LunarColonySimulator.Application.Interfaces;

// O "Core IA" do LCS — calcula a probabilidade de sobrevivência
// da colônia com base no estado atual e no histórico.
public interface ISurvivalCalculator
{
    double CalculateSurvivalScore(Colony colony);
    string GenerateRecommendation(Colony colony);
}
