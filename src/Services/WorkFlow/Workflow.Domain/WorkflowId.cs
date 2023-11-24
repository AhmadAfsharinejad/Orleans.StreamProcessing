#pragma warning disable CS8618

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct WorkflowId
{
    [Id(0)] public string Value { get; }

    public WorkflowId()
    {
    }

    public WorkflowId(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public WorkflowId(Guid id)
    {
        Value = id.ToString();
    }

    public static implicit operator string(WorkflowId id) => id.Value;
    public static explicit operator WorkflowId(string id) => new(id);
    public static implicit operator Guid(WorkflowId id) => Guid.Parse(id.Value);
    public static explicit operator WorkflowId(Guid id) => new(id.ToString());
}