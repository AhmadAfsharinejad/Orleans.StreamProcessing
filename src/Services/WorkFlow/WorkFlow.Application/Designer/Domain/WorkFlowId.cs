using StronglyTypedIds;

namespace Workflow.Application.Designer.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct WorkflowId
{
    public WorkflowId(){}
    
    public static implicit operator string(WorkflowId id) => id.Value;
    public static explicit operator WorkflowId(string id) => new(id);
}