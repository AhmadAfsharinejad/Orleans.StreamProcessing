namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginId
{
    [Id(0)] public Guid Value { get; }

    public PluginId()
    {
    }

    public PluginId(Guid value)
    {
        Value = value;
    }
    
    public static implicit operator Guid(PluginId id) => id.Value;
    public static explicit operator PluginId(Guid id) => new(id);
    public static implicit operator string(PluginId id) => id.Value.ToString();
    public static explicit operator PluginId(string id) => new(Guid.Parse(id));
}