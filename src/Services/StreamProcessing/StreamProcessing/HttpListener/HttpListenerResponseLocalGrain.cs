using System.Collections.Concurrent;
using System.Net;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Placement;
using Orleans.Runtime;
using StreamProcessing.HttpListener.Domain;
using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.HttpResponse.Domain;
using StreamProcessing.PluginCommon;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;

#pragma warning disable CS4014
#pragma warning disable CS1998

namespace StreamProcessing.HttpListener;

[PreferLocalPlacement]
[Reentrant]
internal sealed class HttpListenerResponseLocalGrain : Grain, IHttpListenerResponseLocalGrain
{
    private readonly IPluginOutputCaller _pluginOutputCaller;
    private readonly ConcurrentDictionary<Guid, HttpListenerContext> _httpListenerContextDictionary = new();
    private readonly ILogger<HttpListenerResponseLocalGrain> _logger;
    public HttpListenerResponseLocalGrain(IPluginOutputCaller pluginOutputCaller,
        ILogger<HttpListenerResponseLocalGrain> logger)
    {
        _pluginOutputCaller = pluginOutputCaller ?? throw new ArgumentNullException(nameof(pluginOutputCaller));
        _logger =  logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task CallOutput([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecord record,
        [Immutable] HttpListenerContext httpListenerContext,
        GrainCancellationToken cancellationToken)
    {
        var reqId = Guid.NewGuid();
        _logger.HttpRequestReceived(reqId);

        RequestContext.Set(HttpListenerConsts.ListenerGrainId, this.GetPrimaryKey());
        RequestContext.Set(HttpListenerConsts.RequestId, reqId);

        _httpListenerContextDictionary.TryAdd(reqId, httpListenerContext);

        //Dont await response
        _pluginOutputCaller.CallOutputs(pluginContext, record, cancellationToken);
    }

    public async Task SetResponse([Immutable] HttpResponseTuple responseTuple,
        GrainCancellationToken cancellationToken)
    {
        var reqId = (Guid)RequestContext.Get(HttpListenerConsts.RequestId);

        if (_httpListenerContextDictionary.TryRemove(reqId, out var httpListenerContext))
        {
            var response = httpListenerContext.Response;

            foreach (var header in responseTuple.Headers)
            {
                response.AddHeader(header.Key, header.Value);
            }

            // returning response 
            if (responseTuple.ContentBytes is not null)
            {
                response.Close(responseTuple.ContentBytes, false);
            }
            else
            {
                response.Close();
            }
        }

        _logger.HttpResponseSent(reqId);
    }
}

public static partial class Log
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Debug,
        Message = "HttpRequest {ID} Received.")]
    public static partial void HttpRequestReceived(
        this ILogger logger, Guid id);

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "HttpResponse {ID} Sent.")]
    public static partial void HttpResponseSent(
        this ILogger logger, Guid id);
}