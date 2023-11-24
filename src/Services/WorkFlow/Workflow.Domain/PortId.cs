using StronglyTypedIds;

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
[StronglyTypedId(backingType: StronglyTypedIdBackingType.String, StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public partial struct PortId
{
    public PortId(){}
    
    public static implicit operator string(PortId id) => id.Value;
    public static explicit operator PortId(string id) => new(id);
}