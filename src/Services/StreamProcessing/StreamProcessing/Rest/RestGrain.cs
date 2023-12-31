﻿using System.Runtime.CompilerServices;
using Orleans.Concurrency;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.Rest.Interfaces;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.Rest;

namespace StreamProcessing.Rest;

[StatelessWorker]
[Reentrant]
internal sealed class RestGrain : Grain, IRestGrain
{
    private readonly IPluginOutputCaller _pluginOutputCaller;
    private readonly IPluginConfigFetcher<RestConfig> _pluginConfigFetcher;
    private readonly IRestService _restService;
    private readonly IRestOutputFieldTypeGetter _restOutputFieldTypeGetter;
    private HttpClient? _httpClient;
    private IReadOnlyDictionary<string, FieldType>? _outputFieldTypes;

    public RestGrain(IPluginOutputCaller pluginOutputCaller,
        IPluginConfigFetcher<RestConfig> pluginConfigFetcher,
        IRestService restService,
        IRestOutputFieldTypeGetter restOutputFieldTypeGetter)
    {
        _pluginOutputCaller = pluginOutputCaller ?? throw new ArgumentNullException(nameof(pluginOutputCaller));
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _restService = restService ?? throw new ArgumentNullException(nameof(restService));
        _restOutputFieldTypeGetter = restOutputFieldTypeGetter ?? throw new ArgumentNullException(nameof(restOutputFieldTypeGetter));
    }
    
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"RestGrain Activated  {this.GetGrainId()}");
        return base.OnActivateAsync(cancellationToken);
    }

    public override async Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        Dispose();

        await base.OnDeactivateAsync(reason, cancellationToken);
    }

    private void Dispose()
    {
        if (_httpClient is null)
        {
            return;
        }

        _httpClient.Dispose();
        _httpClient = null;
    }

    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecords pluginRecords,
        GrainCancellationToken cancellationToken)
    {
        var config = await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);
        Init(pluginContext, config);

        var records = new List<PluginRecord>(pluginRecords.Records.Count);

        foreach (var pluginRecord in pluginRecords.Records)
        {
            var record = await _restService.Call(_httpClient!, config, pluginRecord, cancellationToken.CancellationToken);

            records.Add(record);
        }

        await _pluginOutputCaller.CallOutputs(GetOutPluginContext(pluginContext), records, cancellationToken);
    }

    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecord pluginRecord,
        GrainCancellationToken cancellationToken)
    {
        var config = await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);
        Init(pluginContext, config);

        var record = await _restService.Call(_httpClient!, config, pluginRecord, cancellationToken.CancellationToken);

        await _pluginOutputCaller.CallOutputs(GetOutPluginContext(pluginContext), record, cancellationToken);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PluginExecutionContext GetOutPluginContext(PluginExecutionContext pluginContext)
    {
        return pluginContext with { InputFieldTypes = _outputFieldTypes };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Init(PluginExecutionContext pluginContext, RestConfig config)
    {
        if (_httpClient is not null)
        {
            return;
        }

        _outputFieldTypes = _restOutputFieldTypeGetter.GetOutputs(pluginContext, config);

        _httpClient = new HttpClient();
    }
}