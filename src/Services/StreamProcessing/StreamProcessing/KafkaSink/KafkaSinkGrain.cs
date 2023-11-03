using System.Runtime.CompilerServices;
using Orleans.Concurrency;
using StreamProcessing.KafkaSink.Domain;
using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;

namespace StreamProcessing.KafkaSink;

[StatelessWorker]
[Reentrant]
internal sealed class KafkaSinkGrain : PluginGrain, IKafkaSinkGrain
{
    private readonly IPluginConfigFetcher<KafkaSinkConfig> _pluginConfigFetcher;
    private readonly IKafkaSinkServiceFactory _kafkaSinkServiceFactory;
    private bool _hasBeenInitialized;
    private IKafkaSinkService? _KafkaSinkService;

    public KafkaSinkGrain(IPluginConfigFetcher<KafkaSinkConfig> pluginConfigFetcher,
        IKafkaSinkServiceFactory kafkaSinkServiceFactory)
    {
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _kafkaSinkServiceFactory = kafkaSinkServiceFactory ?? throw new ArgumentNullException(nameof(kafkaSinkServiceFactory));
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"KafkaSinkGrain Activated {this.GetGrainId()}");
        return base.OnActivateAsync(cancellationToken);
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        _KafkaSinkService?.Dispose();
        _KafkaSinkService = null;
        return base.OnDeactivateAsync(reason, cancellationToken);
    }

    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecords pluginRecords,
        GrainCancellationToken cancellationToken)
    {
        await Init(pluginContext);

        foreach (var pluginRecord in pluginRecords.Records)
        {
            _KafkaSinkService!.Produce(pluginRecord);
        }
    }

    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecord pluginRecord,
        GrainCancellationToken cancellationToken)
    {
        await Init(pluginContext);

        _KafkaSinkService!.Produce(pluginRecord);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async Task Init(PluginExecutionContext pluginContext)
    {
        if (_hasBeenInitialized) return;

        var config = await _pluginConfigFetcher.GetConfig(pluginContext.ScenarioId, pluginContext.PluginId);

        _KafkaSinkService = _kafkaSinkServiceFactory.Create(config);
        _KafkaSinkService.BuildProducer();

        _hasBeenInitialized = true;
    }
}