using StreamProcessing.KafkaSink.Interfaces;
using Workflow.Domain.Plugins.KafkaSink;

namespace StreamProcessing.KafkaSink.Logic;

internal sealed class KafkaSinkServiceFactory : IKafkaSinkServiceFactory
{
    private readonly IKafkaProducerFactory _kafkaProducerFactory;

    public KafkaSinkServiceFactory(IKafkaProducerFactory kafkaProducerFactory)
    {
        _kafkaProducerFactory = kafkaProducerFactory ?? throw new ArgumentNullException(nameof(kafkaProducerFactory));
    }
    
    public IKafkaSinkService Create(KafkaSinkConfig config)
    {
        return new KafkaSinkService(_kafkaProducerFactory, config);
    }
}