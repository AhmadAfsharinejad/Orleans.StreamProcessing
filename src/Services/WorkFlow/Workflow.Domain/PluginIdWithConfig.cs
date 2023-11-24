namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginIdWithConfig
{
    public PluginIdWithConfig(PluginId Id, IPluginConfig Config)
    {
        this.Id = Id;
        this.Config = Config;
    }

    [Id(0)]
    public PluginId Id { get; set; }
    [Id(1)]
    public IPluginConfig Config { get; set; }
}