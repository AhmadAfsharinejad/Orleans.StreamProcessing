﻿using System.Data;
using System.Runtime.CompilerServices;
using Orleans.Concurrency;
using StreamProcessing.Filter.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain.Plugins.Filter;

namespace StreamProcessing.Filter;

[StatelessWorker]
[Reentrant]
internal sealed class FilterGrain : PluginGrain, IFilterGrain
{
    private readonly IPluginOutputCaller _pluginOutputCaller;
    private readonly IPluginConfigFetcher<FilterConfig> _pluginConfigFetcher;
    private readonly IFilterService _filterService;

    public FilterGrain(IPluginOutputCaller pluginOutputCaller,
        IPluginConfigFetcher<FilterConfig> pluginConfigFetcher,
        IFilterService filterService)
    {
        _pluginOutputCaller = pluginOutputCaller ?? throw new ArgumentNullException(nameof(pluginOutputCaller));
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _filterService = filterService ?? throw new ArgumentNullException(nameof(filterService));
    }
    
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"FilterGrain Activated  {this.GetGrainId()}");
        return base.OnActivateAsync(cancellationToken);
    }
    
    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext, 
        [Immutable] PluginRecords pluginRecords, 
        GrainCancellationToken cancellationToken)
    {
        var config = await GetConfig(pluginContext);
        
        var records = new List<PluginRecord>(pluginRecords.Records.Count);

        foreach (var pluginRecord in pluginRecords.Records)
        {
            if (_filterService.Satisfy(pluginRecord, config.Constraint, pluginContext.InputFieldTypes!))
            {
                records.Add(pluginRecord);
            }
        }

        await _pluginOutputCaller.CallOutputs(pluginContext, records, cancellationToken);
    }

    [ReadOnly]
    public async Task Compute(PluginExecutionContext pluginContext, 
        PluginRecord pluginRecord, 
        GrainCancellationToken cancellationToken)
    {
        var config = await GetConfig(pluginContext);

        if (_filterService.Satisfy(pluginRecord, config.Constraint, pluginContext.InputFieldTypes!))
        {
            await _pluginOutputCaller.CallOutputs(pluginContext, pluginRecord, cancellationToken);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task<FilterConfig> GetConfig(PluginExecutionContext pluginContext)
    {
        if (pluginContext.InputFieldTypes is null) throw new NoNullAllowedException("'InputFieldTypes' can't be null.");

        return await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);
    }
}