using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.KafkaSink;

internal sealed class KafkaSinkGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.KafkaSink);
    public Type GrainInterface => typeof(IKafkaSinkGrain);
}