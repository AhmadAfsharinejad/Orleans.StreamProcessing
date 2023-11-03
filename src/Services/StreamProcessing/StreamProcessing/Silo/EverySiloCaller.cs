using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;

namespace StreamProcessing.Silo;

internal sealed class EverySiloCaller : IEverySiloCaller
{
    private readonly IGrainFactory _grainFactory;

    public EverySiloCaller(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
    }

    public async Task Start([Immutable] Type startingPluginType,
        [Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        var localSiloGrainIds = await GetLocalSiloGrainIds();

        foreach (var localSiloGrainId in localSiloGrainIds)
        {
            var grain = _grainFactory.GetGrain<ILocalSiloGrain>(localSiloGrainId);
            await grain.StartPlugin(startingPluginType, pluginContext, cancellationToken);
        }
    }

    private async Task<IReadOnlyCollection<Guid>> GetLocalSiloGrainIds()
    {
        var coordinator = _grainFactory.GetGrain<ILocalGrainCoordinator>(SiloConsts.CoordinatorGrainId);
        return await coordinator.GetAllLocalSiloGrainIds();
    }
}