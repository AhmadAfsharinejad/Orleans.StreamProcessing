using Workflow.Domain.Plugins.KafkaSink;

namespace StreamProcessing.KafkaSink.Interfaces;

internal interface IKafkaSinkServiceFactory
{
    IKafkaSinkService Create(KafkaSinkConfig config);
}