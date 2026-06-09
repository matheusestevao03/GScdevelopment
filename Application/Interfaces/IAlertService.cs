using LunarColonySimulator.Domain.Entities;

namespace LunarColonySimulator.Application.Interfaces;

public interface IAlertService
{
    void Raise(Alert alert);
    IReadOnlyList<Alert> History { get; }
}
