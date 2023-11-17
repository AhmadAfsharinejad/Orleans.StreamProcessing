namespace Workflow.Domain;

public record struct PluginIdWithConfig(PluginId Id, IPluginConfig Config);
