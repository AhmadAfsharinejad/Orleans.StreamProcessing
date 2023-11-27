using Microsoft.Extensions.Logging;

namespace StreamProcessing.PluginCommon;

internal abstract class PluginGrain : Grain
{
    protected readonly ILogger _logger;

    protected PluginGrain(ILogger logger)
    {
        _logger = logger;
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{PluginName} - {GrainID} Activated.",GetType().Name,this.GetGrainId());
        return base.OnActivateAsync(cancellationToken);
    }
}