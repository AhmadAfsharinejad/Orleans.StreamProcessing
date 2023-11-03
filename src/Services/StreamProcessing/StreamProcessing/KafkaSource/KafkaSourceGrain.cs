using Orleans.Concurrency;
using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;

namespace StreamProcessing.KafkaSource;

[StatelessWorker]
internal sealed class KafkaSourceGrain: Grain, IKafkaSourceGrain
{
    private readonly IEverySiloCaller _everySiloCaller;

    public KafkaSourceGrain(IEverySiloCaller everySiloCaller)
    {
        _everySiloCaller = everySiloCaller ?? throw new ArgumentNullException(nameof(everySiloCaller));
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
        await _everySiloCaller.Start(typeof(IKafkaSourceLocalGrain), pluginContext, cancellationToken);
    }
}