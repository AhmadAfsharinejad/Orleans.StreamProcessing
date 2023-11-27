using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;

namespace StreamProcessing.HttpListener;

[StatelessWorker]
internal sealed class HttpListenerGrain : PluginGrain, IHttpListenerGrain
{
    private readonly IEverySiloCaller _everySiloCaller;

    public HttpListenerGrain(IEverySiloCaller everySiloCaller,ILogger<HttpListenerGrain> logger) : base(logger)
    {
        _everySiloCaller = everySiloCaller ?? throw new ArgumentNullException(nameof(everySiloCaller));
    }

    [ReadOnly]
    public async Task Start([Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        await _everySiloCaller.Start(typeof(IHttpListenerLocalGrain), pluginContext, cancellationToken);
    }
}