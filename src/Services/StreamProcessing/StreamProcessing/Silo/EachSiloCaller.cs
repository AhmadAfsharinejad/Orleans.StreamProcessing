using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;
using System.Linq;

namespace StreamProcessing.Silo;

internal sealed class EachSiloCaller : IEachSiloCaller
{
    private readonly IGrainFactory _grainFactory;

    public EachSiloCaller(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
    }

    public async Task Start([Immutable] Type startingPluginType,
        [Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        var coordinator = _grainFactory.GetGrain<ILocalGrainCoordinator>(SiloConsts.CoordinatorGrainId);
        var ids = await coordinator.GetAllLocalSiloGrainIds();

        foreach (var id in ids)
        {
            var grain = _grainFactory.GetGrain<ILocalSiloGrain>(id);
#pragma warning disable CS4014
            //Dont await source plugins
            grain.StartPlugin(startingPluginType, pluginContext, cancellationToken);
#pragma warning restore CS4014
        }
    }

    public async Task Start([Immutable] Type startingPluginType,
        [Immutable] PluginExecutionContext pluginContext,
        [Immutable] List<string> keyExtensions,
        GrainCancellationToken cancellationToken)
    {
        var coordinator = _grainFactory.GetGrain<ILocalGrainCoordinator>(SiloConsts.CoordinatorGrainId);
        var ids = (await coordinator.GetAllLocalSiloGrainIds()).ToList();

        for (int i = 0, count = 0; count < keyExtensions.Count; i = (i + 1) % ids.Count, count++)
        {
            var id = ids[i];
            var keyExtension = keyExtensions[count];
            
            var grain = _grainFactory.GetGrain<ILocalSiloGrain>(id);
#pragma warning disable CS4014
            //Dont await source plugins
            grain.StartPlugin(startingPluginType, pluginContext, keyExtension, cancellationToken);
#pragma warning restore CS4014
        }
    }
}