using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.SqlExecutor.Domain;

[Immutable]
public record struct SqlExecutorConfig : IStreamPluginConfig
{
    public string ConnectionString { get; set; }
    public IReadOnlyCollection<DmlCommand>? DmlCommands { get; set; }
    public DqlCommand? DqlCommand { get; set; }
    public RecordJoinType JoinType { get; set; }
    //TODO Parallel or single Dml execute
    //TODO Transaction
}