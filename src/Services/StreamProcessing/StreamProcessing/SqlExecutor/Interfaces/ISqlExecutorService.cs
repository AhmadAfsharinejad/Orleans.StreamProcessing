using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.SqlExecutor;

namespace StreamProcessing.SqlExecutor.Interfaces;

internal interface ISqlExecutorService
{
    IAsyncEnumerable<PluginRecord> Execute(IStreamDbConnection connection,
        IStreamDbCommand command,
        SqlExecutorConfig config,
        PluginRecord? record,
        CancellationToken cancellationToken);
}