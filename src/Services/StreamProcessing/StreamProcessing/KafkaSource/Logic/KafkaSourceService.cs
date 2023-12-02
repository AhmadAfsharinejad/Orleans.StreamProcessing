using Confluent.Kafka;
using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.KafkaSource;

namespace StreamProcessing.KafkaSource.Logic;

internal sealed class KafkaSourceService : IKafkaSourceService
{
    public IEnumerable<PluginRecord> Consume(KafkaSourceConfig config, int partitionId,
        CancellationToken cancellationToken)
    {
        var consumerConfig = GetConsumerConfig(config);
        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        consumer.Assign(new TopicPartitionOffset(config.Topic, partitionId, Offset.Beginning));

        while (!cancellationToken.IsCancellationRequested)
        {
            var result = consumer.Consume(cancellationToken);
            yield return new PluginRecord(new Dictionary<string, object>
                { { config.OutputFieldName, result?.Message?.Value! } });
        }
    }

    private static ConsumerConfig GetConsumerConfig(KafkaSourceConfig config)
    {
        return new ConsumerConfig
        {
            BootstrapServers = config.BootstrapServers,
            GroupId = config.GroupId,
            EnableAutoCommit = false,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
}