using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Placement;
using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain.Plugins.HttpListener;

namespace StreamProcessing.HttpListener;

[KeepAlive]
[PreferLocalPlacement]
internal sealed class HttpListenerLocalGrain : Grain, IHttpListenerLocalGrain
{
    private readonly IGrainFactory _grainFactory;
    private readonly IPluginConfigFetcher<HttpListenerConfig> _pluginConfigFetcher;
    private readonly IHttpListenerService _httpListenerService;
    private readonly IHttpListenerOutputFieldTypeGetter _httpListenerOutputFieldTypeGetter;
    private readonly IHttpListenerResponseLocalGrain _httpListenerResponseLocalGrain;
    private readonly ILogger<HttpListenerLocalGrain> _logger;
    public HttpListenerLocalGrain(IGrainFactory grainFactory,
        IPluginConfigFetcher<HttpListenerConfig> pluginConfigFetcher,
        IHttpListenerService httpListenerService,
        IHttpListenerOutputFieldTypeGetter httpListenerOutputFieldTypeGetter,ILogger<HttpListenerLocalGrain> logger)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _httpListenerService = httpListenerService ?? throw new ArgumentNullException(nameof(httpListenerService));
        _httpListenerOutputFieldTypeGetter = httpListenerOutputFieldTypeGetter ??
                                             throw new ArgumentNullException(nameof(httpListenerOutputFieldTypeGetter));
        _logger = logger  ?? throw new ArgumentNullException(nameof(logger));;
        _httpListenerResponseLocalGrain = _grainFactory.GetGrain<IHttpListenerResponseLocalGrain>(Guid.NewGuid());
    }

    [ReadOnly]
    public async Task Start([Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        var config = await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);

        var outPluginContext = pluginContext with
        {
            InputFieldTypes = _httpListenerOutputFieldTypeGetter.GetOutputs(config)
        };

        await foreach (var recordListenerContextTuple in _httpListenerService.Listen(config,
                           cancellationToken.CancellationToken))
        {
            await _httpListenerResponseLocalGrain
                .CallOutput(outPluginContext, recordListenerContextTuple.Record,
                    recordListenerContextTuple.HttpListenerContext, cancellationToken);
        }
    }
}
