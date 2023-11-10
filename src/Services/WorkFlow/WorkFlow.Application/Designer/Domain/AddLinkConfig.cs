namespace Workflow.Application.Designer.Domain;

public record struct AddLinkConfig(LinkId Id, PluginIdWithPort Source, PluginIdWithPort Target);
