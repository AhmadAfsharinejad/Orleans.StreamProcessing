using Confluent.Kafka;
using Workflow.Domain.Plugins.KafkaSink;

namespace StreamProcessing.KafkaSink.Interfaces;

internal interface IKafkaProducerFactory
{
    IProducer<string, string> Create(KafkaSinkConfig config);
}