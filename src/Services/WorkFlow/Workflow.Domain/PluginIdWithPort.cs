namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginIdWithPort(PluginId Id, PortId Port);