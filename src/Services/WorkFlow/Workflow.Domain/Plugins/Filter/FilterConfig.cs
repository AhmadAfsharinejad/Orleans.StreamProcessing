using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.Filter;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.Filter)]
public record struct FilterConfig : IPluginConfig
{
    [Id(0)]
    public IConstraint Constraint { get; init; }
}