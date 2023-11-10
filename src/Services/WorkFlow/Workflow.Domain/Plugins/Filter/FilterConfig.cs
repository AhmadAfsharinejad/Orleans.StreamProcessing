namespace Workflow.Domain.Plugins.Filter;

public record struct FilterConfig : IPluginConfig
{
    public IConstraint Constraint { get; init; }
}