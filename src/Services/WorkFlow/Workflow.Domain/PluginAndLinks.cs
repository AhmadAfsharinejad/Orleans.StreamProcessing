namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginAndLinks(IReadOnlyCollection<Plugin> Plugins, IReadOnlyCollection<Link> Links);