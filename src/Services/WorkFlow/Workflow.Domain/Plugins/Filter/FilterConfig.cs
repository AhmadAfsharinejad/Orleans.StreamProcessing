using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.Filter;

[Config(PluginTypeNames.Filter)]
public record struct FilterConfig : IPluginConfig
{
    public IConstraint Constraint { get; init; }
}