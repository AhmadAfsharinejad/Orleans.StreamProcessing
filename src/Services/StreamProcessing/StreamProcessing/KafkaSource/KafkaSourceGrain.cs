using Orleans.Concurrency;
using Orleans.Runtime;
using StreamProcessing.KafkaSource.Domain;
using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.Silo.Interfaces;
using Workflow.Domain.Plugins.KafkaSource;

namespace StreamProcessing.KafkaSource;

internal sealed class KafkaSourceGrain: Grain, IKafkaSourceGrain
{
    private readonly IKafkaPartition _kafkaPartition;
    private readonly IPluginConfigFetcher<KafkaSourceConfig> _pluginConfigFetcher;
    private readonly IIterativeSiloCaller _iterativeSiloCaller;

    public KafkaSourceGrain(IKafkaPartition kafkaPartition,
        IPluginConfigFetcher<KafkaSourceConfig> pluginConfigFetcher,
        IIterativeSiloCaller iterativeSiloCaller)
    {
        _kafkaPartition = kafkaPartition ?? throw new ArgumentNullException(nameof(kafkaPartition));
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _iterativeSiloCaller = iterativeSiloCaller ?? throw new ArgumentNullException(nameof(iterativeSiloCaller));
    }
    
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"KafkaSourceGrain Activated {this.GetGrainId()}");
        return base.OnActivateAsync(cancellationToken);
    }

    [ReadOnly]
    public async Task Start([Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        var config = await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);

        RequestContext.Set(KafkaSourceConsts.KafkaSourceGrainId, this.GetPrimaryKey());

        foreach (var partitionId in _kafkaPartition.GetPartitionIds(config))
        {
            RequestContext.Set(KafkaSourceConsts.KafkaSourcePartitionId, partitionId);
            await _iterativeSiloCaller.Start(typeof(IKafkaSourceLocalGrain), pluginContext, cancellationToken);
        }
    }
}