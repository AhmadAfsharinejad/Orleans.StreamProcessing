#pragma warning disable CS8618

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PortId
{
    [Id(0)] public string Value { get; }

    public PortId()
    {
    }

    public PortId(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static implicit operator string(PortId id) => id.Value;
    public static explicit operator PortId(string id) => new(id);
}