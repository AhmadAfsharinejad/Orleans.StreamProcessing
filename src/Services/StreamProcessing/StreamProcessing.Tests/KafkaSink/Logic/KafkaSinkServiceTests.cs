using System.Collections.Generic;
using Confluent.Kafka;
using NSubstitute;
using StreamProcessing.KafkaSink.Domain;
using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.KafkaSink.Logic;
using StreamProcessing.PluginCommon.Domain;
using Xunit;

namespace StreamProcessing.Tests.KafkaSink.Logic;

public class KafkaSinkServiceTests
{
    private readonly IKafkaProducerFactory _kafkaProducerFactory;

    public KafkaSinkServiceTests()
    {
        _kafkaProducerFactory = Substitute.For<IKafkaProducerFactory>();
    }

    [Fact]
    public void Produce_ShouldProduceWithStaticKey_WhenStaticMessageKeyFieldNameIsNotNull()
    {
        // Arrange
        var config = new KafkaSinkConfig
        {
            Topic = "topic",
            StaticMessageKeyFieldName = "k11",
            MessageKeyFieldName = "f1",
            StaticMessageValueFieldName = "v22"
        };
        var record = new PluginRecord(new Dictionary<string, object> { { "f1", "k1" }, { "f2", "v2" } });
        
        var producer = Substitute.For<IProducer<string?, string?>>();
        _kafkaProducerFactory.Create(config).Returns(producer);
        
        var sut = new KafkaSinkService(_kafkaProducerFactory, config);
        sut.BuildProducer();

        // Act
        sut.Produce(record);

        // Assert
        producer.Received(1).Produce(config.Topic, 
            Arg.Is<Message<string?, string?>>(x => x.Key == "k11" && x.Value == "v22"));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Produce_ShouldProduceWithFieldKeyValue_WhenStaticMessageKeyFieldNameIsNullOrWhiteSpace(string? staticMessageKeyFieldName)
    {
        // Arrange
        var config = new KafkaSinkConfig
        {
            Topic = "topic",
            StaticMessageKeyFieldName = staticMessageKeyFieldName,
            MessageKeyFieldName = "f1",
            StaticMessageValueFieldName = "v22"
        };
        var record = new PluginRecord(new Dictionary<string, object> { { "f1", "k1" }, { "f2", "v2" } });
        
        var producer = Substitute.For<IProducer<string?, string?> >();
        _kafkaProducerFactory.Create(config).Returns(producer);
        
        var sut = new KafkaSinkService(_kafkaProducerFactory, config);
        sut.BuildProducer();

        // Act
        sut.Produce(record);

        // Assert
        producer.Received(1).Produce(config.Topic, 
            Arg.Is<Message<string?, string?>>(x => x.Key == "k1" && x.Value == "v22"));
    }
    
    [Fact]
    public void Produce_ShouldProduceWithNullKey_WhenStaticMessageKeyFieldNameAndMessageKeyFieldNameAreNull()
    {
        // Arrange
        var config = new KafkaSinkConfig
        {
            Topic = "topic",
            StaticMessageValueFieldName = "v22"
        };
        var record = new PluginRecord(new Dictionary<string, object> { { "f1", "k1" }, { "f2", "v2" } });
        
        var producer = Substitute.For<IProducer<string?, string?>>();
        _kafkaProducerFactory.Create(config).Returns(producer);
        
        var sut = new KafkaSinkService(_kafkaProducerFactory, config);
        sut.BuildProducer();

        // Act
        sut.Produce(record);

        // Assert
        producer.Received(1).Produce(config.Topic, 
            Arg.Is<Message<string?, string?>>(x => x.Key == null && x.Value == "v22"));
    }
    
    [Fact]
    public void Produce_ShouldProduceWithStaticValue_WhenStaticMessageValueFieldNameIsNotNull()
    {
        // Arrange
        var config = new KafkaSinkConfig
        {
            Topic = "topic",
            StaticMessageKeyFieldName = "k11",
            StaticMessageValueFieldName = "v22",
            MessageValueFieldName = "f2"
        };
        var record = new PluginRecord(new Dictionary<string, object> { { "f1", "k1" }, { "f2", "v2" } });
        
        var producer = Substitute.For<IProducer<string?, string?> >();
        _kafkaProducerFactory.Create(config).Returns(producer);
        
        var sut = new KafkaSinkService(_kafkaProducerFactory, config);
        sut.BuildProducer();

        // Act
        sut.Produce(record);

        // Assert
        producer.Received(1).Produce(config.Topic, 
            Arg.Is<Message<string?, string?>>(x => x.Key == "k11" && x.Value == "v22"));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Produce_ShouldProduceWithFieldValue_WhenStaticMessageValueFieldNameIsNullOrWhiteSpace(string staticMessageValueFieldName)
    {
        // Arrange
        var config = new KafkaSinkConfig
        {
            Topic = "topic",
            StaticMessageKeyFieldName = "k11",
            StaticMessageValueFieldName = staticMessageValueFieldName,
            MessageValueFieldName = "f2"
        };
        var record = new PluginRecord(new Dictionary<string, object> { { "f1", "k1" }, { "f2", "v2" } });
        
        var producer = Substitute.For<IProducer<string?, string?> >();
        _kafkaProducerFactory.Create(config).Returns(producer);
        
        var sut = new KafkaSinkService(_kafkaProducerFactory, config);
        sut.BuildProducer();

        // Act
        sut.Produce(record);

        // Assert
        producer.Received(1).Produce(config.Topic, 
            Arg.Is<Message<string?, string?>>(x => x.Key == "k11" && x.Value == "v2"));
    }
}