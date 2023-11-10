using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.SqlExecutor;

public record struct SqlExecutorConfig : IPluginConfig
{
    public string ConnectionString { get; init; }
    public IReadOnlyCollection<DmlCommand>? DmlCommands { get; set; }
    public DqlCommand? DqlCommand { get; init; }
    public RecordJoinType JoinType { get; init; }
    //TODO Parallel or single Dml execute
    //TODO Transaction
}