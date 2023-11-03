using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.Silo.Interfaces;

/// <summary>
/// Run source plugin in all silos
/// </summary>
internal interface IEverySiloCaller
{
    Task Start([Immutable] Type startingPluginType, 
        [Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken);
}