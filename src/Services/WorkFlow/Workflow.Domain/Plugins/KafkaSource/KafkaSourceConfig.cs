namespace Workflow.Domain.Plugins.KafkaSource;

public record struct KafkaSourceConfig : IPluginConfig
{
    public string BootstrapServers { get; init; }
    public string Topic { get; init; }
    public string GroupId { get; init; }
    public string OutputFieldName { get; init; }
}