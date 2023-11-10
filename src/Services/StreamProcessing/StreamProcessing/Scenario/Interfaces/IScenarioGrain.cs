using Orleans.Concurrency;
using StreamProcessing.Common;
using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain;

namespace StreamProcessing.Scenario.Interfaces;

internal interface IScenarioGrain : IGrainWithStringKey
{
    Task AddScenario(ImmutableWrapper<WorkflowDesign> config);
    
    [AlwaysInterleave]
    [ReadOnly]
    Task<ImmutableWrapper<IPluginConfig>> GetPluginConfig(Guid pluginId);

    [AlwaysInterleave]
    [ReadOnly]
    Task<IReadOnlyCollection<PluginTypeWithId>> GetOutputTypes(Guid pluginId);
}