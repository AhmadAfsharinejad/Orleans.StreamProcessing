using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;

namespace StreamProcessing.KafkaSink;

internal sealed class KafkaSinkGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.KafkaSink);
    public Type GrainInterface => typeof(IKafkaSinkGrain);
}