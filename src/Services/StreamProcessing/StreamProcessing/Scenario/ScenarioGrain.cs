using Orleans.Concurrency;
using Orleans.Runtime;
using StreamProcessing.Common;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Scenario.Interfaces;
using StreamProcessing.Storage;
using Workflow.Domain;

namespace StreamProcessing.Scenario;

internal sealed class ScenarioGrain : Grain, IScenarioGrain
{
    private readonly IPersistentState<WorkflowDesign> _configState;

    public ScenarioGrain(
        [PersistentState(stateName: "scenarioConfigState", storageName: StorageConsts.StorageName)] IPersistentState<WorkflowDesign> configState)
    {
        _configState = configState;
    }
    
    public async Task AddScenario(ImmutableWrapper<WorkflowDesign> config)
    {
        _configState.State = config.Config; 
        await _configState.WriteStateAsync();
    }
    
    [ReadOnly]
    public Task<ImmutableWrapper<IPluginConfig>> GetPluginConfig(Guid pluginId)
    {
        var config = _configState.State.PluginAndLinks.Plugins.FirstOrDefault(x => x.Id == pluginId);

        if (config.Equals(default))
        {
            throw new Exception($"Config for plugin '{pluginId}' not exist.");
        }

        return Task.FromResult(new ImmutableWrapper<IPluginConfig>(config.Config));
    }
    
    [ReadOnly]
    public Task<IReadOnlyCollection<PluginTypeWithId>> GetOutputTypes(Guid pluginId)
    {
        var outputIds = _configState.State.PluginAndLinks.Links
            .Where(x => x.Source.Id == pluginId)
            .Select(x => x.Target.Id).ToHashSet();

        var outputs = _configState.State.PluginAndLinks.Plugins
            .Where(x => outputIds.Contains(x.Id))
            .Select(x => new PluginTypeWithId(x.Id, x.PluginTypeId))
            .ToArray();

        return Task.FromResult(outputs as IReadOnlyCollection<PluginTypeWithId>);
    }
}