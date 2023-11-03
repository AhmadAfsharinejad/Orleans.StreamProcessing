using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.Silo.Interfaces;

/// <summary>
/// Run source plugin in silos Iteratively
/// </summary>
internal interface IIterativeSiloCaller
{
    Task Start([Immutable] Type startingPluginType, 
        [Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken);
}