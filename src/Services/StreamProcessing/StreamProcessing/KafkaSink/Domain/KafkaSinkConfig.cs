using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.KafkaSink.Domain;

[Immutable]
public record struct KafkaSinkConfig : IPluginConfig
{
    public string BootstrapServers { get; init; }
    public string Topic { get; init; }
    public string? StaticMessageKeyFieldName { get; init; }
    public string? MessageKeyFieldName { get; init; }
    public string? StaticMessageValueFieldName { get; init; }
    public string? MessageValueFieldName { get; init; }
}