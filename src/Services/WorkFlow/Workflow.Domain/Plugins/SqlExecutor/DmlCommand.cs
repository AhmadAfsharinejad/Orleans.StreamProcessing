namespace Workflow.Domain.Plugins.SqlExecutor;

public record struct DmlCommand
{
    public string CommandText { get; set; }
    public IReadOnlyCollection<string> ParameterFields { get; set; }
}