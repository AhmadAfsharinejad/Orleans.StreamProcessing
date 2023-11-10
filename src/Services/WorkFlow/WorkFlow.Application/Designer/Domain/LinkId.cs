using StronglyTypedIds;

namespace Workflow.Application.Designer.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct LinkId
{
    public LinkId(){}
    
    public static implicit operator string(LinkId id) => id.Value;
    public static explicit operator LinkId(string id) => new(id);
}