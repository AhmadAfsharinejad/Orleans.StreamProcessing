using Workflow.Domain.Plugins.Attributes;
using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.Map;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.Map)]
public record struct MapConfig : IPluginConfig
{
    [Id(0)]
    public IReadOnlyCollection<StreamField> OutputColumns { get; set; }
    [Id(1)]
    public string Code { get; init; }
    [Id(2)]
    public string FullClassName { get; init; }
    [Id(3)]
    public string FunctionName { get; init; }
}