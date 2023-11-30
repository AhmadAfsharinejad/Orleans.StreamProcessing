using StreamProcessing.HttpListener.Domain;
using Workflow.Domain.Plugins.HttpListener;

namespace StreamProcessing.HttpListener.Interfaces;

internal interface IHttpListenerService
{
    IAsyncEnumerable<RecordListenerContextTuple> Listen(HttpListenerConfig config, CancellationToken cancellationToken);
}