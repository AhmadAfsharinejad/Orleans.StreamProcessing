using Confluent.Kafka;
using StreamProcessing.KafkaSource.Domain;
using StreamProcessing.KafkaSource.Interfaces;

namespace StreamProcessing.KafkaSource.Logic;

internal sealed class KafkaPartition : IKafkaPartition
{
    public IEnumerable<int> GetPartitionIds(KafkaSourceConfig config)
    {
        using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = config.BootstrapServers }).Build();
        var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));

        var topic = meta.Topics.SingleOrDefault(t => t.Topic == config.Topic);

        return topic!.Partitions.Select(x => x.PartitionId);
    }
}