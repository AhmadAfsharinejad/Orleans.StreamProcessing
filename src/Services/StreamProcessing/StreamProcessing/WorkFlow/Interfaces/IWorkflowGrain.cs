using Orleans.Concurrency;
using StreamProcessing.Common.Domain;
using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain;

namespace StreamProcessing.WorkFlow.Interfaces;

internal interface IWorkflowGrain : IGrainWithStringKey
{
    Task Add(ImmutableWrapper<WorkflowDesign> configWrapper);
    
    [AlwaysInterleave]
    [ReadOnly]
    Task<ImmutableWrapper<IPluginConfig>> GetPluginConfig(Guid pluginId);

    [AlwaysInterleave]
    [ReadOnly]
    Task<IReadOnlyCollection<PluginTypeWithId>> GetOutputTypes(Guid pluginId);
}