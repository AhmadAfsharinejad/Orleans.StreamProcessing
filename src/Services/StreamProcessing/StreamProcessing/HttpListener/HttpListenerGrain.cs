using Orleans.Concurrency;
using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;

namespace StreamProcessing.HttpListener;

[StatelessWorker]
internal sealed class HttpListenerGrain : Grain, IHttpListenerGrain
{
    private readonly IEverySiloCaller _everySiloCaller;

    public HttpListenerGrain(IEverySiloCaller everySiloCaller)
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
        await _everySiloCaller.Start(typeof(IHttpListenerLocalGrain), pluginContext, cancellationToken);
    }
}