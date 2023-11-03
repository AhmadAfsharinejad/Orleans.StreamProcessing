using NSubstitute;
using StreamProcessing.KafkaSink;
using StreamProcessing.KafkaSink.Domain;
using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using Xunit;

namespace StreamProcessing.Tests.KafkaSink;

public class KafkaSinkGrainTests
{
    private readonly IKafkaSinkGrain _sut;
    private readonly IPluginConfigFetcher<KafkaSinkConfig> _pluginConfigFetcher;
    private readonly IKafkaSinkServiceFactory _kafkaSinkServiceFactory;
    
    public KafkaSinkGrainTests()
    {
        _pluginConfigFetcher = Substitute.For<IPluginConfigFetcher<KafkaSinkConfig>>();
        _kafkaSinkServiceFactory = Substitute.For<IKafkaSinkServiceFactory>();
        _sut = new KafkaSinkGrain(_pluginConfigFetcher, _kafkaSinkServiceFactory);
    }
    
    [Fact]
    public void Compute_ShouldCallBuildProducerOneTime_WhenCallMultipleTimes()
    {
        //Arrange
        var config = new KafkaSinkConfig();

        _pluginConfigFetcher.GetConfig(default, default).ReturnsForAnyArgs(config);

        var kafkaSinkService = Substitute.For<IKafkaSinkService>();
        _kafkaSinkServiceFactory.Create(config).Returns(kafkaSinkService);
            
        //Act
        _sut.Compute(default, new PluginRecord(), default!);
        _sut.Compute(default, new PluginRecords(), default!);

        //Assert
        kafkaSinkService.ReceivedWithAnyArgs(1).BuildProducer();
        _kafkaSinkServiceFactory.ReceivedWithAnyArgs(1).Create(default);
    }
}