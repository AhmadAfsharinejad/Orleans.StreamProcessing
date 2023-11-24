using Workflow.Domain.Plugins.Attributes;
using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.SqlExecutor;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.SqlExecutor)]
public record struct SqlExecutorConfig : IPluginConfig
{
    [Id(0)]
    public string ConnectionString { get; init; }
    [Id(1)]
    public IReadOnlyCollection<DmlCommand>? DmlCommands { get; set; }
    [Id(2)]
    public DqlCommand? DqlCommand { get; set; }
    [Id(3)]
    public RecordJoinType JoinType { get; init; }
    //TODO Parallel or single Dml execute
    //TODO Transaction
}