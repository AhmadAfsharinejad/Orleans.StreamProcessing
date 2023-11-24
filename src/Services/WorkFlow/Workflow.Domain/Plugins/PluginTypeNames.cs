namespace Workflow.Domain.Plugins;

[GenerateSerializer]
public enum PluginTypeNames
{
    Random,
    Filter,
    DummyOutput,
    SqlExecutor,
    HttpListener,
    HttpResponse,
    Rest,
    Map,
    KafkaSource,
    KafkaSink
}