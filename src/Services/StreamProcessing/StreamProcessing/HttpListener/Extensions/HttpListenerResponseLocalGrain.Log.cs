using Microsoft.Extensions.Logging;

namespace StreamProcessing.HttpListener.Extensions;

internal static class HttpListenerResponseLocalGrainLog
{
    private static readonly Action<ILogger, Guid, Exception> HttpRequestReceivedLog;
    private static readonly Action<ILogger, Guid, Exception> HttpResponseSentLog;

    static HttpListenerResponseLocalGrainLog()
    {
        HttpRequestReceivedLog = LoggerMessage.Define<Guid>(
            logLevel: LogLevel.Debug,
            eventId: 1,
            formatString: "HttpRequest {ID} Received.");

        HttpResponseSentLog = LoggerMessage.Define<Guid>(
            logLevel: LogLevel.Debug,
            eventId: 2,
            formatString: "HttpResponse {ID} Sent.");
    }

    public static void HttpRequestReceived(this ILogger<HttpListenerResponseLocalGrain> logger, Guid id)
    {
        HttpRequestReceivedLog(logger, id, null);
    }

    public static void HttpResponseSent(this ILogger<HttpListenerResponseLocalGrain> logger, Guid id)
    {
        HttpResponseSentLog(logger, id, null);
    }
}