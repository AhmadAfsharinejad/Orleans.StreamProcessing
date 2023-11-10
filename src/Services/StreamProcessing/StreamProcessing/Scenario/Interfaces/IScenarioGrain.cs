using Orleans.Concurrency;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Scenario.Domain;

namespace StreamProcessing.Scenario.Interfaces;

internal interface IScenarioGrain : IGrainWithGuidKey
{
    Task AddScenario(ScenarioConfig config);
    
    [AlwaysInterleave]
    [ReadOnly]
    Task<IStreamPluginConfig> GetPluginConfig(Guid pluginId);

    [AlwaysInterleave]
    [ReadOnly]
    Task<IReadOnlyCollection<PluginTypeWithId>> GetOutputTypes(Guid pluginId);
}