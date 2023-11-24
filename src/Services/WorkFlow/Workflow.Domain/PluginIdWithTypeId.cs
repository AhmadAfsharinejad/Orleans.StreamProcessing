namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginIdWithTypeId
{
    public PluginIdWithTypeId(PluginId Id, PluginTypeId TypeId)
    {
        this.Id = Id;
        this.TypeId = TypeId;
    }
    
    [Id(0)]
    public PluginId Id { get; init;}
    [Id(1)]
    public PluginTypeId TypeId { get; init; }
}
