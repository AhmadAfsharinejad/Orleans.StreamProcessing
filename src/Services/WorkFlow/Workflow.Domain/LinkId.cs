namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct LinkId
{
    [Id(0)] public Guid Value { get; }

    public LinkId()
    {
    }

    public LinkId(Guid value)
    {
        Value = value;
    }

    public static implicit operator Guid(LinkId id) => id.Value;
    public static explicit operator LinkId(Guid id) => new(id);
    public static implicit operator string(LinkId id) => id.Value.ToString();
    public static explicit operator LinkId(string id) => new(Guid.Parse(id));
}