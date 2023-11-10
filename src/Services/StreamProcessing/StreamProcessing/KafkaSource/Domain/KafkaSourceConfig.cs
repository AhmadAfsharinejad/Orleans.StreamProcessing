using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.KafkaSource.Domain;

[Immutable]
public record struct KafkaSourceConfig : IStreamPluginConfig
{
    public string BootstrapServers { get; set; }
    public string Topic { get; set; }
    public string GroupId { get; set; }
    public string OutputFieldName { get; set; }
}