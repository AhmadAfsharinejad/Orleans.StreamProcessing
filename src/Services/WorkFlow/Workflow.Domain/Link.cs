namespace Workflow.Domain;

public record struct Link(LinkId Id, PluginIdWithPort Source, PluginIdWithPort Target);
