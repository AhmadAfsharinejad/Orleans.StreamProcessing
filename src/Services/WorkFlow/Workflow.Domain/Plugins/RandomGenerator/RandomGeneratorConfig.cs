using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.RandomGenerator;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.Random)]
public record struct RandomGeneratorConfig : IPluginConfig
{
    public RandomGeneratorConfig()
    {
        BatchCount = 10;
        Columns = Array.Empty<RandomColumn>();
    }

    [Id(0)]
    public short BatchCount { get; init; }
    [Id(1)]
    public long Count { get; init; }
    [Id(2)]
    public IReadOnlyCollection<RandomColumn> Columns { get; set; }
}