using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Silo.Interfaces;

namespace StreamProcessing.Silo;

internal sealed class IterativeSiloCaller : IIterativeSiloCaller
{
    private readonly IGrainFactory _grainFactory;
    private readonly HashSet<Guid> _calledLocalSiloGrainIds;

    public IterativeSiloCaller(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory ?? throw new ArgumentNullException(nameof(grainFactory));
        _calledLocalSiloGrainIds = new HashSet<Guid>();
    }
    
    public async Task Start([Immutable] Type startingPluginType,
        [Immutable] PluginExecutionContext pluginContext,
        GrainCancellationToken cancellationToken)
    {
        var localSiloGrainIds = await GetLocalSiloGrainIds();

        var notCalledLocalSiloGrainId = GetNotCalledLocalSiloGrainId(localSiloGrainIds);
        
        AddToCalledLocalSiloGrainIds(notCalledLocalSiloGrainId, localSiloGrainIds);

        var grain = _grainFactory.GetGrain<ILocalSiloGrain>(notCalledLocalSiloGrainId);
#pragma warning disable CS4014
        //Dont await source plugins
        grain.StartPlugin(startingPluginType, pluginContext, cancellationToken);
#pragma warning restore CS4014
    }

    private Guid GetNotCalledLocalSiloGrainId(IReadOnlyCollection<Guid> localSiloGrainIds)
    {
        var id = localSiloGrainIds.FirstOrDefault(x => !_calledLocalSiloGrainIds.Contains(x));

        return id == default ? localSiloGrainIds.First() : id;
    }

    private void AddToCalledLocalSiloGrainIds(Guid notCalledLocalSiloGrainId, IReadOnlyCollection<Guid> localSiloGrainIds)
    {
        _calledLocalSiloGrainIds.Add(notCalledLocalSiloGrainId);
        
        if (_calledLocalSiloGrainIds.Count >= localSiloGrainIds.Count)
        {
            _calledLocalSiloGrainIds.Clear();
        }
    }

    private async Task<IReadOnlyCollection<Guid>> GetLocalSiloGrainIds()
    {
        var coordinator = _grainFactory.GetGrain<ILocalGrainCoordinator>(SiloConsts.CoordinatorGrainId);
        return await coordinator.GetAllLocalSiloGrainIds();
    }
}