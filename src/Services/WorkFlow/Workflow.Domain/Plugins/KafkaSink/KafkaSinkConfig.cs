using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.KafkaSink;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.KafkaSink)]
public record struct KafkaSinkConfig : IPluginConfig
{
    [Id(0)]
    public string BootstrapServers { get; init; }
    [Id(1)]
    public string Topic { get; init; }
    [Id(2)]
    public string? StaticMessageKeyFieldName { get; init; }
    [Id(3)]
    public string? MessageKeyFieldName { get; init; }
    [Id(4)]
    public string? StaticMessageValueFieldName { get; init; }
    [Id(5)]
    public string? MessageValueFieldName { get; init; }
}