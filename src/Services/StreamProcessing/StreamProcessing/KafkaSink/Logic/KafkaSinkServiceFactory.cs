using StreamProcessing.KafkaSink.Domain;
using StreamProcessing.KafkaSink.Interfaces;

namespace StreamProcessing.KafkaSink.Logic;

internal sealed class KafkaSinkServiceFactory : IKafkaSinkServiceFactory
{
    public IKafkaSinkService Create(KafkaSinkConfig config)
    {
        return new KafkaSinkService(config);
    }
}