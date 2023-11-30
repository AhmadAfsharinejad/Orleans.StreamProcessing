using Workflow.Domain;

namespace Workflow.Application.PluginFinder.Interfaces;

internal interface IPluginConfigFinder
{
    IPluginConfig GetConfig(PluginTypeId pluginTypeId);
}