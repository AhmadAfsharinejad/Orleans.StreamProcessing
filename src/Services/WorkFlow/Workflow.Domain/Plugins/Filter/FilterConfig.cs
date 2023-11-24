using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.Filter;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.Filter)]
public record struct FilterConfig : IPluginConfig
{
    public IConstraint Constraint { get; init; }
}