using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.SqlExecutor.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.SqlExecutor;

internal sealed class SqlExecutorGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.SqlExecutor);
    public Type GrainInterface => typeof(ISqlExecutorGrain);
}