using Workflow.Domain.Plugins.Attributes;
using Workflow.Domain.Plugins.HttpListener;

namespace Workflow.Domain.Plugins.HttpResponse;

[Config(PluginTypeNames.HttpResponse)]
public record struct HttpResponseConfig : IPluginConfig
{
    public IReadOnlyCollection<HeaderField>? Headers { get; set; }
    public IReadOnlyCollection<KeyValuePair<string, string>>? StaticHeaders { get; set; }
    /// <summary>
    /// We replace ContentFields using string.Format
    /// For example Conent is like: { "age": {0}, "name": {1}}
    /// </summary>
    public string? Content { get; init; }
    public IReadOnlyCollection<string>? ContentFields { get; set; }
    //TODO status
}