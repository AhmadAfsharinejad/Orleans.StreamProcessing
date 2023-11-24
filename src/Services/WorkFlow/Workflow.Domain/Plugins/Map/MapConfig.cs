using Workflow.Domain.Plugins.Attributes;
using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.Map;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.Map)]
public record struct MapConfig : IPluginConfig
{
    public IReadOnlyCollection<StreamField> OutputColumns { get; set; }
    public string Code { get; init; }
    public string FullClassName { get; init; }
    public string FunctionName { get; init; }
}