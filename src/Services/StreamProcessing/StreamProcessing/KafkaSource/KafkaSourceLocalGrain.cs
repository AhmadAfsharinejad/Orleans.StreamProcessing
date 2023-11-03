using Orleans.Concurrency;
using Orleans.Placement;
using StreamProcessing.KafkaSource.Domain;
using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;

namespace StreamProcessing.KafkaSource;

[KeepAlive]
[PreferLocalPlacement]
internal sealed class KafkaSourceLocalGrain : Grain, IKafkaSourceLocalGrain
{
    private readonly IGrainFactory _grainFactory;
    private readonly IPluginConfigFetcher<KafkaSourceConfig> _pluginConfigFetcher;
    private readonly IKafkaSourceService _kafkaSourceService;
    private readonly IPluginOutputCaller _pluginOutputCaller;

    public KafkaSourceLocalGrain(IGrainFactory grainFactory,
        IPluginConfigFetcher<KafkaSourceConfig> pluginConfigFetcher,
        IKafkaSourceService kafkaSourceService,
        IPluginOutputCaller pluginOutputCaller)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _kafkaSourceService = kafkaSourceService ?? throw new ArgumentNullException(nameof(kafkaSourceService));
        _pluginOutputCaller = pluginOutputCaller ?? throw new ArgumentNullException(nameof(pluginOutputCaller));
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"KafkaSourceLocalGrain Activated {this.GetGrainId()}");
        return base.OnActivateAsync(cancellationToken);
    }

    [ReadOnly]
    public async Task Start([Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        var config = await _pluginConfigFetcher.GetConfig(pluginContext.ScenarioId, pluginContext.PluginId);

        var outPluginContext = GetOutPluginContext(pluginContext, config.OutputFieldName);

        foreach (var record in _kafkaSourceService.Consume(config, cancellationToken.CancellationToken))
        {
            await _pluginOutputCaller.CallOutputs(outPluginContext, record, cancellationToken);
        }
    }

    private static PluginExecutionContext GetOutPluginContext(PluginExecutionContext pluginContext, string outputFieldName)
    {
        return pluginContext with { InputFieldTypes = new Dictionary<string, FieldType> { { outputFieldName, FieldType.Text } } };
    }
}