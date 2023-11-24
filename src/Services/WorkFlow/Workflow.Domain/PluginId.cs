using StronglyTypedIds;

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public partial struct PluginId
{
    public PluginId(){}
    
    public static implicit operator Guid(PluginId id) => id.Value;
    public static explicit operator PluginId(Guid id) => new(id);
    public static implicit operator string(PluginId id) => id.Value.ToString();
    public static explicit operator PluginId(string id) => new(Guid.Parse(id));
}