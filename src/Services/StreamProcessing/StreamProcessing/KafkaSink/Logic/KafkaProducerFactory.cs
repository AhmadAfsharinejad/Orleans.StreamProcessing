using Confluent.Kafka;
using StreamProcessing.KafkaSink.Domain;
using StreamProcessing.KafkaSink.Interfaces;

namespace StreamProcessing.KafkaSink.Logic;

internal sealed class KafkaProducerFactory : IKafkaProducerFactory
{
    public IProducer<string?, string?> Create(KafkaSinkConfig config)
    {
        return new ProducerBuilder<string?, string?>(GetProducerConfig(config)).Build();
    }
    
    private ProducerConfig GetProducerConfig(KafkaSinkConfig config)
    {
        return new ProducerConfig
        {
            BootstrapServers = config.BootstrapServers
        };
    }
}