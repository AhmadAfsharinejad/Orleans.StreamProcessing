using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.Rest;

namespace StreamProcessing.Rest.Interfaces;

internal interface IRestRequestCreator
{
    HttpRequestMessage Create(RestConfig config, PluginRecord pluginRecord, CancellationToken cancellationToken);
}