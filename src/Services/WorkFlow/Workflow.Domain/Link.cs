namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct Link(LinkId Id, PluginIdWithPort Source, PluginIdWithPort Target);