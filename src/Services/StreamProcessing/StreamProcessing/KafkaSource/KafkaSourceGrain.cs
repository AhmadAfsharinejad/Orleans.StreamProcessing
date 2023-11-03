using Orleans.Concurrency;
using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;

namespace StreamProcessing.KafkaSource;

[StatelessWorker]
internal sealed class KafkaSourceGrain: Grain, IKafkaSourceGrain
{
    private readonly IEachSiloCaller _eachSiloCaller;

    public KafkaSourceGrain(IEachSiloCaller eachSiloCaller)
    {
        _eachSiloCaller = eachSiloCaller ?? throw new ArgumentNullException(nameof(eachSiloCaller));
    }
    
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"HttpListenerGrain Activated {this.GetGrainId()}");
        return base.OnActivateAsync(cancellationToken);
    }

    [ReadOnly]
    public async Task Start([Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        await _eachSiloCaller.Start(typeof(IKafkaSourceLocalGrain), pluginContext, cancellationToken);
    }
}