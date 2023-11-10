namespace Workflow.Domain;

public record struct PluginAndLinks(IReadOnlyCollection<Plugin> Plugins, IReadOnlyCollection<Link> Links);
