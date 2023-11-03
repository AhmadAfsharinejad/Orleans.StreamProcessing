using StreamProcessing.KafkaSource.Domain;
using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.KafkaSource.Interfaces;

internal interface IKafkaSourceService
{
    IEnumerable<PluginRecord> Consume(KafkaSourceConfig config, CancellationToken cancellationToken);
}