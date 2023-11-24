namespace Workflow.Domain.Plugins.HttpListener;

[Immutable, GenerateSerializer]
public record struct HeaderField(string NameInHeader, string FieldName);