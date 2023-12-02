using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;

namespace StreamProcessing.HttpListener;

[StatelessWorker]
internal sealed class HttpListenerGrain : Grain, IHttpListenerGrain
{
    private readonly IEverySiloCaller _everySiloCaller;
    private readonly ILogger<HttpListenerGrain> _logger;

    public HttpListenerGrain(IEverySiloCaller everySiloCaller,
        ILogger<HttpListenerGrain> logger)
    {
        _everySiloCaller = everySiloCaller ?? throw new ArgumentNullException(nameof(everySiloCaller));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [ReadOnly]
    public async Task Start([Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        await _everySiloCaller.Start(typeof(IHttpListenerLocalGrain), pluginContext, cancellationToken);
    }
}