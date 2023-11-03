using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.KafkaSink.Domain;

[Immutable]
public record struct KafkaSinkConfig : IPluginConfig
{
    public string BootstrapServers { get; set; }
    public string Topic { get; set; }
    public string MessageKeyFieldName { get; set; }
    public string MessageValueFieldName { get; set; }
}