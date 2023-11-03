using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;

namespace StreamProcessing.KafkaSource;

internal sealed class KafkaSourceGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.KafkaSource);
    public Type GrainInterface => typeof(IKafkaSourceGrain);
}