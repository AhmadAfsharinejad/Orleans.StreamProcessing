using StreamProcessing.KafkaSink.Domain;

namespace StreamProcessing.KafkaSink.Interfaces;

internal interface IKafkaSinkServiceFactory
{
    IKafkaSinkService Create(KafkaSinkConfig config);
}