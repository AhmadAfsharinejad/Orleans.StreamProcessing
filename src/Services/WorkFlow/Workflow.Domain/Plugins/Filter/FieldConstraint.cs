namespace Workflow.Domain.Plugins.Filter;

public record struct FieldConstraint : IConstraint
{
    public string FieldName { get; init; }
    public object Value { get; init; }
    public ConstraintOperators Operator { get; init; }
}