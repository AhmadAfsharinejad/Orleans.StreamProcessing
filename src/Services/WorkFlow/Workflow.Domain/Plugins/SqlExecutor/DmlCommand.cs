namespace Workflow.Domain.Plugins.SqlExecutor;

[Immutable, GenerateSerializer]
public record struct DmlCommand
{
    [Id(0)]
    public string CommandText { get; set; }
    [Id(1)]
    public IReadOnlyCollection<string> ParameterFields { get; set; }
}