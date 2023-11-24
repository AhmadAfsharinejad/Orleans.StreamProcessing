namespace Workflow.Domain.Plugins.HttpListener;

[Immutable, GenerateSerializer]
public record struct QueryStringField
{
    public QueryStringField(string NameInQueryString, string FieldName)
    {
        this.NameInQueryString = NameInQueryString;
        this.FieldName = FieldName;
    }

    [Id(0)]
    public string NameInQueryString { get; set; }
    [Id(1)]
    public string FieldName { get; set; }

    public void Deconstruct(out string NameInQueryString, out string FieldName)
    {
        NameInQueryString = this.NameInQueryString;
        FieldName = this.FieldName;
    }
}