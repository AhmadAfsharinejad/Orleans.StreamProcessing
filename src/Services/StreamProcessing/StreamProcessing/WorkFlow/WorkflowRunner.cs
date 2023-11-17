﻿using StreamProcessing.Common;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.WorkFlow.Interfaces;
using Workflow.Domain;

namespace StreamProcessing.WorkFlow;

//TODO add stop - cancellation token
internal class WorkflowRunner : IWorkflowRunner
{
    private readonly IGrainFactory _grainFactory;
    private readonly IPluginGrainFactory _pluginGrainFactory;

    public WorkflowRunner(IGrainFactory grainFactory, IPluginGrainFactory pluginGrainFactory)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _pluginGrainFactory = pluginGrainFactory ?? throw new ArgumentNullException(nameof(pluginGrainFactory));
    }

    public async Task Run(WorkflowDesign config)
    {
        using var tcs = new GrainCancellationTokenSource();
        
        var runTasks = new List<Task>();

        var workflowGrain = _grainFactory.GetGrain<IWorkflowGrain>(config.Id.Value);
        await workflowGrain.Add(new ImmutableWrapper<WorkflowDesign>(config));

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