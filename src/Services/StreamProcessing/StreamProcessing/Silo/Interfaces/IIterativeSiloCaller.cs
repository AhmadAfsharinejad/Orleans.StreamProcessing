using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.Silo.Interfaces;

//Run source plugin in silos Iteratively
internal interface IIterativeSiloCaller
{
    Task Start([Immutable] Type startingPluginType, 
        [Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken);
}