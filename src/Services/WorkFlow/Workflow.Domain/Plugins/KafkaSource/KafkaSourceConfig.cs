using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.KafkaSource;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.KafkaSource)]
public record struct KafkaSourceConfig : IPluginConfig
{
    [Id(0)]
    public string BootstrapServers { get; init; }
    [Id(1)]
    public string Topic { get; init; }
    [Id(2)]
    public string GroupId { get; init; }
    [Id(3)]
    public string OutputFieldName { get; init; }
}