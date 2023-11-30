using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Runtime;
using StreamProcessing.HttpListener.Domain;
using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.HttpResponse.Interfaces;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain.Plugins.HttpResponse;

namespace StreamProcessing.HttpResponse;

[StatelessWorker]
[Reentrant]
internal sealed class HttpResponseGrain : PluginGrain, IHttpResponseGrain
{
    private readonly IPluginConfigFetcher<HttpResponseConfig> _pluginConfigFetcher;
    private readonly IHttpResponseService _httpResponseService;
    private readonly IGrainFactory _grainFactory;
    private readonly ILogger<HttpResponseGrain> _logger;
    public HttpResponseGrain(IPluginConfigFetcher<HttpResponseConfig> pluginConfigFetcher,
        IHttpResponseService httpResponseService,
        IGrainFactory grainFactory,ILogger<HttpResponseGrain> logger)
    {
        _pluginConfigFetcher = pluginConfigFetcher ?? throw new ArgumentNullException(nameof(pluginConfigFetcher));
        _httpResponseService = httpResponseService ?? throw new ArgumentNullException(nameof(httpResponseService));
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _logger =  logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [ReadOnly]
    public async Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecord pluginRecord,
        GrainCancellationToken cancellationToken)
    {
        var config = await _pluginConfigFetcher.GetConfig(pluginContext.WorkFlowId, pluginContext.PluginId);

        var response = _httpResponseService.GetResponse(config, pluginRecord);

        var listenerGrainId = GetListenerGrainId();

        var grain = _grainFactory.GetGrain<IHttpListenerResponseLocalGrain>(listenerGrainId);
        await grain.SetResponse(response, cancellationToken);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Guid GetListenerGrainId()
    {
        return (Guid)RequestContext.Get(HttpListenerConsts.ListenerGrainId);
    }

    [ReadOnly]
    public Task Compute([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecords pluginRecords,
        GrainCancellationToken cancellationToken)
    {
        //TODO DELETE
        throw new NotImplementedException();
    }
}