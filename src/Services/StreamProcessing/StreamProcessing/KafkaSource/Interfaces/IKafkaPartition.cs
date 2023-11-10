using Workflow.Domain.Plugins.KafkaSource;

namespace StreamProcessing.KafkaSource.Interfaces;

internal interface IKafkaPartition
{
    IEnumerable<int> GetPartitionIds(KafkaSourceConfig config);
}