namespace Workflow.Domain.Plugins.HttpListener;

public record struct HttpListenerConfig : IPluginConfig
{
    public string Uri { get; init; }
    public IReadOnlyCollection<HeaderField>? Headers { get; set; }
    public IReadOnlyCollection<QueryStringField>? QueryStrings { get; set; }
    public string? ContentFieldName { get; init; }
}