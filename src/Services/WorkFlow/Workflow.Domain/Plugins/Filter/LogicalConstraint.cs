namespace Workflow.Domain.Plugins.Filter;

[Immutable, GenerateSerializer]
public record struct LogicalConstraint : IConstraint
{
    [Id(0)]
    public IReadOnlyCollection<IConstraint> Constraints { get; set; }
    [Id(1)]
    public ConstraintOperator Operator { get; init; }
}