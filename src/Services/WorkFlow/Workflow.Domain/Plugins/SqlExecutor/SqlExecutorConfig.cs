using Workflow.Domain.Plugins.Attributes;
using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.SqlExecutor;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.SqlExecutor)]
public record struct SqlExecutorConfig : IPluginConfig
{
    public string ConnectionString { get; init; }
    public IReadOnlyCollection<DmlCommand>? DmlCommands { get; set; }
    public DqlCommand? DqlCommand { get; set; }
    public RecordJoinType JoinType { get; init; }
    //TODO Parallel or single Dml execute
    //TODO Transaction
}