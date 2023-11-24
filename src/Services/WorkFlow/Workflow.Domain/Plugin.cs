namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct Plugin
{
    [Id(0)]
    public PluginTypeId PluginTypeId { get; init; }
    [Id(1)]
    public PluginId Id { get; init; }
    [Id(2)]
    public IPluginConfig Config { get; init; }

    public Plugin(PluginTypeId PluginTypeId, PluginId Id, IPluginConfig Config)
    {
        this.PluginTypeId = PluginTypeId;
        this.Id = Id;
        this.Config = Config;
    }
}