namespace Workflow.Domain.Plugins.Filter;

[Immutable, GenerateSerializer]
public record struct FieldConstraint : IConstraint
{
    [Id(0)]
    public string FieldName { get; init; }
    [Id(1)]
    public object Value { get; init; }
    [Id(2)]
    public ConstraintOperators Operator { get; init; }
}