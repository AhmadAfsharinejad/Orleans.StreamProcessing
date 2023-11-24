namespace Workflow.Domain.Plugins.HttpListener;

[Immutable, GenerateSerializer]
public record struct HeaderField
{
    public HeaderField(string NameInHeader, string FieldName)
    {
        this.NameInHeader = NameInHeader;
        this.FieldName = FieldName;
    }

    [Id(0)]
    public string NameInHeader { get; set; }
    [Id(1)]
    public string FieldName { get; set; }

    public void Deconstruct(out string NameInHeader, out string FieldName)
    {
        NameInHeader = this.NameInHeader;
        FieldName = this.FieldName;
    }
}