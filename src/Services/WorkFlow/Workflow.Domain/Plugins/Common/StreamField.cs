namespace Workflow.Domain.Plugins.Common;

[Immutable, GenerateSerializer]
public record struct StreamField
{
    public StreamField(string Name, FieldType Type)
    {
        this.Name = Name;
        this.Type = Type;
    }

    [Id(0)]
    public string Name { get; set; }
    [Id(1)]
    public FieldType Type { get; set; }

    public void Deconstruct(out string Name, out FieldType Type)
    {
        Name = this.Name;
        Type = this.Type;
    }
}