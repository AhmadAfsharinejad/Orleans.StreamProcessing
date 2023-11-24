using Workflow.Domain.Plugins.Attributes;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.HttpListener;

namespace Workflow.Domain.Plugins.Rest;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.Rest)]
public record struct RestConfig : IPluginConfig
{
    [Id(0)]
    public HttpMethod HttpMethod { get; set; }
    [Id(1)]
    public RecordJoinType JoinType { get; set; }
    [Id(2)]
    public string Uri { get; set; }
    [Id(3)]
    public IReadOnlyCollection<HeaderField>? RequestHeaders { get; set; }
    [Id(4)]
    public IReadOnlyCollection<KeyValuePair<string, string>>? RequestStaticHeaders { get; set; }
    [Id(5)]
    public IReadOnlyCollection<QueryStringField>? QueryStrings { get; set; }
    [Id(6)]
    public IReadOnlyCollection<KeyValuePair<string, string>>? StaticQueryStrings { get; set; }

    /// <summary>
    /// We replace ContentFields using string.Format
    /// For example Conent is like: { "age": {0}, "name": {1}}
    /// </summary>
    [Id(7)]
    public string? Content { get; set; }
    [Id(8)]
    public IReadOnlyCollection<string>? ContentFields { get; set; }
    [Id(9)]
    public string? StatusFieldName { get; set; }
    [Id(10)]
    public string? ResponseContentFieldName { get; set; }
    [Id(11)]
    public IReadOnlyCollection<HeaderField>? ResponseHeaders { get; set; }
}