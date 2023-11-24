using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.Rest;

namespace StreamProcessing.Rest.Interfaces;

internal interface IUriReplacer
{
    string GetUri(RestConfig config, PluginRecord pluginRecord);
}