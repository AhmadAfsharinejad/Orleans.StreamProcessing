using StronglyTypedIds;

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public partial struct LinkId
{
    public LinkId(){}
    
    public static implicit operator Guid(LinkId id) => id.Value;
    public static explicit operator LinkId(Guid id) => new(id);
    public static implicit operator string(LinkId id) => id.Value.ToString();
    public static explicit operator LinkId(string id) => new(Guid.Parse(id));
}