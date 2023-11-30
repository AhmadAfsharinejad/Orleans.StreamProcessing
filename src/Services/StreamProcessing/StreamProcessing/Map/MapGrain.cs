using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using StreamProcessing.Map.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.Map;

namespace StreamProcessing.Map;

[StatelessWorker]
[Reentrant]
internal sealed class MapGrain : PluginGrain, IMapGrain
{
    private readonly IPluginOutputCaller _pluginOutputCaller;
    private readonly IPluginConfigFetcher<MapConfig> _pluginConfigFetcher;
    private readonly ICompiler _compiler;
    private bool _hasBeenInitialized;
    private IReadOnlyDictionary<string, FieldType>? _outputFieldTypes;
    private Func<IReadOnlyDictionary<string, object>, IReadOnlyDictionary<string, object>>? _func;
    private readonly ILogger<MapGrain> _logger;

    public MapGrain(IPluginOutputCaller pluginOutputCaller,
        IPluginConfigFetcher<MapConfig> pluginConfigFetcher,
        ICompiler compiler, ILogger<MapGrain> logger)
    {
        _pluginOutputCaller = pluginOutputCaller ?? throw new ArgumentNullException(nameof(pluginOutputCaller));
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecords pluginRecords,
        GrainCancellationToken cancellationToken)
    {
        await Init(pluginContext);

        var records = new List<PluginRecord>(pluginRecords.Records.Count);

        foreach (var pluginRecord in pluginRecords.Records)
        {
            var resultRecord = _func!(pluginRecord.Record);
            records.Add(new PluginRecord(resultRecord));
        }

        await _pluginOutputCaller.CallOutputs(GetOutPluginContext(pluginContext), records, cancellationToken);
    }

    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecord pluginRecord,
        GrainCancellationToken cancellationToken)
    {
        await Init(pluginContext);

        var resultRecord = _func!(pluginRecord.Record);

        await _pluginOutputCaller.CallOutputs(GetOutPluginContext(pluginContext),
            new PluginRecord(resultRecord),
            cancellationToken);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task Init(PluginExecutionContext pluginContext)
    {
        if (_hasBeenInitialized) return;

        var config = await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);

        _func = _compiler.CreateFunction(config.Code, config.FullClassName, config.FunctionName);

        _outputFieldTypes = config.OutputColumns.ToDictionary(x => x.Name, y => y.Type);

        _hasBeenInitialized = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PluginExecutionContext GetOutPluginContext(PluginExecutionContext pluginContext)
    {
        return pluginContext with { InputFieldTypes = _outputFieldTypes };
    }
}