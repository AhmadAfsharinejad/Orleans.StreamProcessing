using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Placement;
using Orleans.Runtime;
using StreamProcessing.KafkaSource.Domain;
using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.KafkaSource;

namespace StreamProcessing.KafkaSource;

[KeepAlive]
[PreferLocalPlacement]
internal sealed class KafkaSourceLocalGrain : Grain, IKafkaSourceLocalGrain
{
    private readonly IGrainFactory _grainFactory;
    private readonly IPluginConfigFetcher<KafkaSourceConfig> _pluginConfigFetcher;
    private readonly IKafkaSourceService _kafkaSourceService;
    private readonly IPluginOutputCaller _pluginOutputCaller;
    private readonly ILogger<KafkaSourceLocalGrain> _logger;
    public KafkaSourceLocalGrain(IGrainFactory grainFactory,
        IPluginConfigFetcher<KafkaSourceConfig> pluginConfigFetcher,
        IKafkaSourceService kafkaSourceService,
        IPluginOutputCaller pluginOutputCaller,ILogger<KafkaSourceLocalGrain> logger)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _kafkaSourceService = kafkaSourceService ?? throw new ArgumentNullException(nameof(kafkaSourceService));
        _pluginOutputCaller = pluginOutputCaller ?? throw new ArgumentNullException(nameof(pluginOutputCaller));
        _logger = logger  ?? throw new ArgumentNullException(nameof(logger)) ;

    }

    [ReadOnly]
    public async Task Start([Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        var config = await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);

        var outPluginContext = GetOutPluginContext(pluginContext, config.OutputFieldName);

        var partitionId = GetKafkaPartitionId();
        
        foreach (var record in _kafkaSourceService.Consume(config, partitionId, cancellationToken.CancellationToken))
        {
            await _pluginOutputCaller.CallOutputs(outPluginContext, record, cancellationToken);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetKafkaPartitionId()
    {
        return (int)RequestContext.Get(KafkaSourceConsts.KafkaSourcePartitionId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static PluginExecutionContext GetOutPluginContext(PluginExecutionContext pluginContext, string outputFieldName)
    {
        return pluginContext with { InputFieldTypes = new Dictionary<string, FieldType> { { outputFieldName, FieldType.Text } } };
    }
}