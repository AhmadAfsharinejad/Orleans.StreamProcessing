namespace Workflow.Domain.Plugins.Common;

[Immutable, GenerateSerializer]
public record struct StreamField(string Name, FieldType Type);