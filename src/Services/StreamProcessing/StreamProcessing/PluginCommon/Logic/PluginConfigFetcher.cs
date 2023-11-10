using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.Scenario.Interfaces;
using Workflow.Domain;

namespace StreamProcessing.PluginCommon.Logic;

internal sealed class PluginConfigFetcher<TConfig> : IPluginConfigFetcher<TConfig>
where TConfig : IPluginConfig
{
    private TConfig? _config;
    private readonly IGrainFactory _grainFactory;

    public PluginConfigFetcher(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
    }

    public async Task<TConfig> GetConfig(WorkflowId workflowId, PluginId pluginId)
    {
        if (!(_config is null || _config!.Equals(default(TConfig)))) return _config;

        var scenarioGrain = _grainFactory.GetGrain<IScenarioGrain>(workflowId);
        var config = await scenarioGrain.GetPluginConfig(pluginId);

        if (config.Config is not TConfig tConfig) throw new InvalidCastException($"Can't cast plugin '{pluginId}' to specific type.");

        _config = tConfig;
        return _config;
    }
}