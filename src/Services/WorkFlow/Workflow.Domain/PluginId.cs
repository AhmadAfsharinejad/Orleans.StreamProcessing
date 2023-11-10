using StronglyTypedIds;

namespace Workflow.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid)]
public partial struct PluginId
{
    public PluginId(){}
    
    public static implicit operator Guid(PluginId id) => id.Value;
    public static explicit operator PluginId(Guid id) => new(id);
}