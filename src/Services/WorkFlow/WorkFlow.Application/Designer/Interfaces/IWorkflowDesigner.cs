using Workflow.Domain;

namespace Workflow.Application.Designer.Interfaces;

public interface IWorkflowDesigner
{
    void AddPlugin(PluginTypeId pluginTypeId, PluginId pluginId);
    void RemovePlugin(PluginId pluginId);
    IPluginConfig GetPluginConfig(PluginId pluginId);
    void SetPluginConfig(PluginId pluginId, IPluginConfig config);
    void AddLink(Link config);
    void RemoveLink(LinkId linkId);
    PluginAndLinks GetPluginAndLinks();
}