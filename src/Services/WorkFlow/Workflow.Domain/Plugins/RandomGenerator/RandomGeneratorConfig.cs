using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.RandomGenerator;

[Config(PluginTypeNames.Random)]
public record struct RandomGeneratorConfig : IPluginConfig
{
    public RandomGeneratorConfig()
    {
        BatchCount = 10;
        Columns = Array.Empty<RandomColumn>();
    }

    public short BatchCount { get; init; }
    public long Count { get; init; }
    public IReadOnlyCollection<RandomColumn> Columns { get; set; }
}