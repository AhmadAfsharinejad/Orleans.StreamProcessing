using Microsoft.Extensions.Logging;

namespace StreamProcessing.HttpListener;

internal sealed partial class HttpListenerResponseLocalGrain
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Debug,
        Message = "HttpRequest {ID} Received.")] 
    partial void HttpRequestReceivedLog(Guid id);
    

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "HttpResponse {ID} Sent.")]
    partial void HttpResponseSentLog(Guid id);
}