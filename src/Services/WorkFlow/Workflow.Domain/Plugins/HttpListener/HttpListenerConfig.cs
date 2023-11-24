using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.HttpListener;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.HttpListener)]
public record struct HttpListenerConfig : IPluginConfig
{
    [Id(0)]
    public string Uri { get; init; }
    [Id(1)]
    public IReadOnlyCollection<HeaderField>? Headers { get; set; }
    [Id(2)]
    public IReadOnlyCollection<QueryStringField>? QueryStrings { get; set; }
    [Id(3)]
    public string? ContentFieldName { get; init; }
}