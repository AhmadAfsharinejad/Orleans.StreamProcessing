using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.Silo.Interfaces;

//Run source plugin in all silos
internal interface IEverySiloCaller
{
    Task Start([Immutable] Type startingPluginType, 
        [Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken);
}