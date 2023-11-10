using StreamProcessing.Common;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.Scenario.Interfaces;
using Workflow.Domain;

namespace StreamProcessing.Scenario;

//TODO add stop - cancellation token
internal class ScenarioRunner : IScenarioRunner
{
    private readonly IGrainFactory _grainFactory;
    private readonly IPluginGrainFactory _pluginGrainFactory;

    public ScenarioRunner(IGrainFactory grainFactory, IPluginGrainFactory pluginGrainFactory)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _pluginGrainFactory = pluginGrainFactory ?? throw new ArgumentNullException(nameof(pluginGrainFactory));
    }

    public async Task Run(WorkflowDesign config)
    {
        using var tcs = new GrainCancellationTokenSource();
        
        var runTasks = new List<Task>();

        var scenarioGrain = _grainFactory.GetGrain<IScenarioGrain>(config.Id.Value);
        await scenarioGrain.AddScenario(new ImmutableWrapper<WorkflowDesign>(config));

        foreach (var plugin in FindSourcePlugins(config))
        {
            var grain = _pluginGrainFactory.GetOrCreateSourcePlugin(plugin.PluginTypeId, plugin.Id.Value);
            var runTask = grain.Start(
                new PluginExecutionContext(config.Id, plugin.Id, null), tcs.Token);
            runTasks.Add(runTask);
        }

        await Task.WhenAll(runTasks);
    }

    private static IEnumerable<Plugin> FindSourcePlugins(WorkflowDesign config)
    {
        var destinationIds = config.PluginAndLinks.Links.Select(x => x.Target.Id).ToHashSet();
        return config.PluginAndLinks.Plugins.Where(x => !destinationIds.Contains(x.Id));
    }
}