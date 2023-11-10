using StronglyTypedIds;

namespace Workflow.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct PluginId
{
    public PluginId(){}
    
    public static implicit operator string(PluginId id) => id.Value;
    public static explicit operator PluginId(string id) => new(id);
}