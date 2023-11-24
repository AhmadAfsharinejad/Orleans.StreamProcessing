namespace Workflow.Domain.Plugins.SqlExecutor;

[Immutable, GenerateSerializer]
public record struct DqlCommand
{
    public string CommandText { get; init; }
    public IReadOnlyCollection<string> ParameterFields { get; set; }
    public IReadOnlyCollection<DqlField> OutputFields { get; set; }
}