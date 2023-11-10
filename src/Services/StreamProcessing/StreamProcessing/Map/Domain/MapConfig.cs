using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Map.Domain;

[Immutable]
public record struct MapConfig : IStreamPluginConfig
{
    public IReadOnlyCollection<StreamField> OutputColumns { get; set; }
    public string Code { get; set; }
    public string FullClassName { get; set; }
    public string FunctionName { get; set; }
}