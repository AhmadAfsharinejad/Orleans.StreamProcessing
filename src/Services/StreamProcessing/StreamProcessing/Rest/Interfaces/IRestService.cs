using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.Rest;

namespace StreamProcessing.Rest.Interfaces;

internal interface IRestService
{
    Task<PluginRecord> Call(HttpClient httpClient,
        RestConfig config,
        PluginRecord pluginRecord,
        CancellationToken cancellationToken);
}