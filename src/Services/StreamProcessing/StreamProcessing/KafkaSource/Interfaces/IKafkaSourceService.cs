using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.KafkaSource;

namespace StreamProcessing.KafkaSource.Interfaces;

internal interface IKafkaSourceService
{
    IEnumerable<PluginRecord> Consume(KafkaSourceConfig config, int partitionId, CancellationToken cancellationToken);
}