using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.RandomGenerator;

[Immutable, GenerateSerializer]
public record struct RandomColumn
{
    [Id(0)]
    public StreamField Field { get; init;}
    [Id(1)]
    public RandomType Type { get; init; }

    public RandomColumn(StreamField Field, RandomType Type)
    {
        this.Field = Field;
        this.Type = Type;
    }
}