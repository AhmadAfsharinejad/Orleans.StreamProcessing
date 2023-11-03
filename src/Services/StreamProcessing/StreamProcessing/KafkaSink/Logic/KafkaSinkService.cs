using Confluent.Kafka;
using StreamProcessing.KafkaSink.Domain;
using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.KafkaSink.Logic;

internal sealed class KafkaSinkService : IKafkaSinkService
{
    private readonly KafkaSinkConfig _config;
    private IProducer<string, string>? _producer;

    public KafkaSinkService(KafkaSinkConfig config)
    {
        _config = config;
    }

    public void BuildProducer()
    {
        var producerConfig = GetProducerConfig();
        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
    }

    private ProducerConfig GetProducerConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = _config.BootstrapServers
        };
    }

    public void Produce(PluginRecord pluginRecord)
    {
        _producer!.Produce(_config.Topic, new Message<string, string>
        {
            Key = pluginRecord.Record[_config.MessageKeyFieldName].ToString()!,
            Value = pluginRecord.Record[_config.MessageValueFieldName].ToString()!
        });
    }

    public void Dispose()
    {
        _producer?.Dispose();
    }
}