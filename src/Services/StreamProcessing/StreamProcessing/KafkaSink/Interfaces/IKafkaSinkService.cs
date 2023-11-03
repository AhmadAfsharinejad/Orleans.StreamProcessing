using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.KafkaSink.Interfaces;

internal interface IKafkaSinkService : IDisposable
{
    void BuildProducer();
    void Produce(PluginRecord pluginRecord);
}