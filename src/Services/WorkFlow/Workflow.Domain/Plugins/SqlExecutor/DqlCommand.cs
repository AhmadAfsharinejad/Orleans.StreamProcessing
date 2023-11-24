namespace Workflow.Domain.Plugins.SqlExecutor;

[Immutable, GenerateSerializer]
public record struct DqlCommand
{
    [Id(0)]
    public string CommandText { get; init; }
    [Id(1)]
    public IReadOnlyCollection<string> ParameterFields { get; set; }
    [Id(2)]
    public IReadOnlyCollection<DqlField> OutputFields { get; set; }
}