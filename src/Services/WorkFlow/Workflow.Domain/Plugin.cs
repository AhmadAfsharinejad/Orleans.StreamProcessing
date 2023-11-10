namespace Workflow.Domain;

public record struct Plugin(PluginTypeId PluginTypeId, PluginId Id, IPluginConfig Config);
