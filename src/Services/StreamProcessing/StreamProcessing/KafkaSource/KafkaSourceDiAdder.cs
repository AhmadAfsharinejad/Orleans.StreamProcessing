using Microsoft.Extensions.DependencyInjection;
using StreamProcessing.Di;
using StreamProcessing.KafkaSource.Interfaces;
using StreamProcessing.KafkaSource.Logic;
using StreamProcessing.PluginCommon.Interfaces;

namespace StreamProcessing.KafkaSource;

internal sealed class KafkaSourceDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection collection)
    {
        collection.AddSingleton<IPluginGrainIntroducer, KafkaSourceGrainIntroducer>();
        collection.AddSingleton<IKafkaSourceService, KafkaSourceService>();
    }
}