using Microsoft.Extensions.DependencyInjection;
using StreamProcessing.Di;
using StreamProcessing.KafkaSink.Interfaces;
using StreamProcessing.KafkaSink.Logic;
using StreamProcessing.PluginCommon.Interfaces;

namespace StreamProcessing.KafkaSink;

internal sealed class KafkaSinkDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection collection)
    {
        collection.AddSingleton<IPluginGrainIntroducer, KafkaSinkGrainIntroducer>();
        collection.AddSingleton<IKafkaSinkServiceFactory, KafkaSinkServiceFactory>();
    }
}