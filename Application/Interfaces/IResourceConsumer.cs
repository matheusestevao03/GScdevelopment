using LunarColonySimulator.Domain.ValueObjects;

namespace LunarColonySimulator.Application.Interfaces;

public interface IResourceConsumer
{
    string ConsumerName { get; }
    ResourceLevel ConsumePerTick(TimeSpan tickDuration);
}
