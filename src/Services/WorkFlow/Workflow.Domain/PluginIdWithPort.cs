namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginIdWithPort
{
    public PluginIdWithPort(PluginId Id, PortId Port)
    {
        this.Id = Id;
        this.Port = Port;
    }
    
    [Id(0)]
    public PluginId Id { get; init;}
    [Id(1)]
    public PortId Port { get; init; }
}