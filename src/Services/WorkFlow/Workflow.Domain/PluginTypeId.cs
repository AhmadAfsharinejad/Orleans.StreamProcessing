using StronglyTypedIds;

namespace Workflow.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct PluginTypeId
{
    public PluginTypeId(){}
    
    public static implicit operator string(PluginTypeId id) => id.Value;
    public static explicit operator PluginTypeId(string id) => new(id);
}