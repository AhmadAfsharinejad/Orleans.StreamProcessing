using StronglyTypedIds;

namespace Workflow.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct PortId
{
    public PortId(){}
    
    public static implicit operator string(PortId id) => id.Value;
    public static explicit operator PortId(string id) => new(id);
}