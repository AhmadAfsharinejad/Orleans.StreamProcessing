using StreamProcessing.HttpResponse.Interfaces;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.HttpResponse;

internal sealed class HttpResponseGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.HttpResponse);
    public Type GrainInterface => typeof(IHttpResponseGrain);
}