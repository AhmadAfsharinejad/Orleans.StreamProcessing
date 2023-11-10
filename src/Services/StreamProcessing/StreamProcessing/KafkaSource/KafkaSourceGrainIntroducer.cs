using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.KafkaSource;

internal sealed class KafkaSourceGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.KafkaSource);
    public Type GrainInterface => typeof(IKafkaSourceGrain);
}