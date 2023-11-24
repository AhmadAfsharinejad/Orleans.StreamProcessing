namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginIdWithConfig(PluginId Id, IPluginConfig Config);