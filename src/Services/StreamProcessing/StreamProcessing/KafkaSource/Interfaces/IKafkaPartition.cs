using StreamProcessing.KafkaSource.Domain;

namespace StreamProcessing.KafkaSource.Interfaces;

internal interface IKafkaPartition
{
    IEnumerable<int> GetPartitionIds(KafkaSourceConfig config);
}