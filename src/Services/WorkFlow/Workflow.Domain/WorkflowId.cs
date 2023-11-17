using StronglyTypedIds;

namespace Workflow.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String, StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.NewtonsoftJson | StronglyTypedIdConverter.SystemTextJson)]
public partial struct WorkflowId
{
    public WorkflowId(){}
    public WorkflowId(Guid id)
    {
        Value = id.ToString();
    }
    public static implicit operator string(WorkflowId id) => id.Value;
    public static explicit operator WorkflowId(string id) => new(id);
    public static implicit operator Guid(WorkflowId id) => Guid.Parse(id.Value);
    public static explicit operator WorkflowId(Guid id) => new(id.ToString());
}