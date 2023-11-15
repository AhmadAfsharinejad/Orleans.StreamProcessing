using System.Collections.Concurrent;
using System.Net;
using Orleans.Placement;
using Orleans.Runtime;
using StreamProcessing.HttpListener.Domain;
using StreamProcessing.HttpListener.Interfaces;
using StreamProcessing.HttpResponse.Domain;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;

#pragma warning disable CS4014
#pragma warning disable CS1998

namespace StreamProcessing.HttpListener;

[PreferLocalPlacement]
internal sealed class HttpListenerResponseLocalGrain : Grain, IHttpListenerResponseLocalGrain
{
    private readonly IPluginOutputCaller _pluginOutputCaller;
    private readonly ConcurrentDictionary<Guid,HttpListenerContext> _httpListenerContextDictionary = new(); 
    
    public HttpListenerResponseLocalGrain(IPluginOutputCaller pluginOutputCaller)
    {
        _pluginOutputCaller = pluginOutputCaller ?? throw new ArgumentNullException(nameof(pluginOutputCaller));
    }

    public async Task CallOutput([Immutable] PluginExecutionContext pluginContext,
        [Immutable] PluginRecord record,
        [Immutable] HttpListenerContext httpListenerContext,
        GrainCancellationToken cancellationToken)
    {
        var reqId = Guid.NewGuid();

        RequestContext.Set(HttpListenerConsts.ListenerGrainId, this.GetPrimaryKey());
        RequestContext.Set(HttpListenerConsts.RequestId, reqId);
        
        _httpListenerContextDictionary.TryAdd(reqId,httpListenerContext);
        //Dont await response
        _pluginOutputCaller.CallOutputs(pluginContext, record, cancellationToken);
    }

    public async Task SetResponse([Immutable] HttpResponseTuple responseTuple,
        GrainCancellationToken cancellationToken)
    {
        var reqId = (Guid) RequestContext.Get(HttpListenerConsts.RequestId);
        
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
                response.Close(responseTuple.ContentBytes,false);
            }
            else
            {
                response.Close();            
            }
        }
        // deactivate grain on finish.
    }
}