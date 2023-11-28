using Microsoft.Extensions.Logging;

namespace StreamProcessing.PluginCommon;

internal abstract class LoggableGrain : Grain
{
    
    protected readonly ILogger _logger;

    protected LoggableGrain(ILogger logger)
    {
        _logger = logger;
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("{GrainID} Activated.",this.GetGrainId());
        return base.OnActivateAsync(cancellationToken);
    }
}