using Microsoft.Extensions.Logging;

namespace StreamProcessing.PluginCommon;

internal abstract class PluginGrain : LoggableGrain
{
    protected PluginGrain(ILogger logger) : base(logger)
    {
    }
}