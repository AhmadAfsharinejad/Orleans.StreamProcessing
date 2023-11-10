namespace Workflow.Domain.Plugins.RandomGenerator;

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