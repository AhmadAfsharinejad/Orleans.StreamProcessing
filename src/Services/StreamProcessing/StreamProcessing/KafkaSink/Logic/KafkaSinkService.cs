using System.Runtime.CompilerServices;
using Confluent.Kafka;
using StreamProcessing.KafkaSink.Domain;
using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.KafkaSink.Logic;

internal sealed class KafkaSinkService : IKafkaSinkService
{
    private readonly IKafkaProducerFactory _kafkaProducerFactory;
    private readonly KafkaSinkConfig _config;
    private IProducer<string, string>? _producer;

    public KafkaSinkService(IKafkaProducerFactory kafkaProducerFactory, KafkaSinkConfig config)
    {
        _kafkaProducerFactory = kafkaProducerFactory ?? throw new ArgumentNullException(nameof(kafkaProducerFactory));
        _config = config;
    }

    public void BuildProducer()
    {
        _producer = _kafkaProducerFactory.Create(_config);
    }

    public void Produce(PluginRecord pluginRecord)
    {
        _producer!.Produce(_config.Topic, new Message<string, string>
        {
            Key = GetMessageKey(pluginRecord),
            Value = GetMessageValue(pluginRecord)
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetMessageValue(PluginRecord pluginRecord)
    {
        if (!string.IsNullOrWhiteSpace(_config.StaticMessageValueFieldName))
        {
            return _config.StaticMessageValueFieldName;
        }
        
        return pluginRecord.Record[_config.MessageValueFieldName!].ToString()!;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private string GetMessageKey(PluginRecord pluginRecord)
    {
        if (!string.IsNullOrWhiteSpace(_config.StaticMessageKeyFieldName))
        {
            return _config.StaticMessageKeyFieldName;
        }
        
        if (string.IsNullOrWhiteSpace(_config.MessageKeyFieldName))
        {
            return string.Empty;
        }

        return pluginRecord.Record[_config.MessageKeyFieldName!].ToString()!;
    }
    
    public void Dispose()
    {
        _producer?.Dispose();
    }
}