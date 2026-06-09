using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Application.Interfaces;

// Contrato para qualquer módulo que produza recursos (painéis solares,
// reatores, estufas, recicladores de água).
public interface IResourceProducer
{
    string ProducerName { get; }
    ResourceLevel ProducePerTick(TimeSpan tickDuration);
}
