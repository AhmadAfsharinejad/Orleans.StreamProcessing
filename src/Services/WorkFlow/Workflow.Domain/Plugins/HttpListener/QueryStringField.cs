namespace Workflow.Domain.Plugins.HttpListener;

[Immutable, GenerateSerializer]
public record struct QueryStringField(string NameInQueryString, string FieldName);