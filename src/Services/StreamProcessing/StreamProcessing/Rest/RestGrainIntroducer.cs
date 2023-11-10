using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.Rest.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.Rest;

internal sealed class RestGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.Rest);
    public Type GrainInterface => typeof(IRestGrain);
}