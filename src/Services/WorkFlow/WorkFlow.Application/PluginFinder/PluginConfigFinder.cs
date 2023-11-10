using Workflow.Application.PluginFinder.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.PluginFinder;

internal sealed class PluginConfigFinder : IPluginConfigFinder
{
    public IPluginConfig GetConfig(PluginTypeId pluginTypeId)
    {
        //TODO
        throw new Exception();
    }
}