using Confluent.Kafka;
using StreamProcessing.KafkaSink.Domain;

namespace StreamProcessing.KafkaSink.Interfaces;

internal interface IKafkaProducerFactory
{
    IProducer<string, string> Create(KafkaSinkConfig config);
}