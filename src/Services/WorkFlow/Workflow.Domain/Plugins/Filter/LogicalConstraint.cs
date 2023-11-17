﻿namespace Workflow.Domain.Plugins.Filter;

public record struct LogicalConstraint : IConstraint
{
    public IReadOnlyCollection<IConstraint> Constraints { get; set; }
    public ConstraintOperator Operator { get; init; }
}