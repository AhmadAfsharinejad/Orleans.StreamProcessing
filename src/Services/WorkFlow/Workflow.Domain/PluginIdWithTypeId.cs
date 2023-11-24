namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginIdWithTypeId(PluginId Id, PluginTypeId TypeId);
