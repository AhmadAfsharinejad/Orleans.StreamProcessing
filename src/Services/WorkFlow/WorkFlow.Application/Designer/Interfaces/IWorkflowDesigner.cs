using Workflow.Application.Designer.Domain;
using Workflow.Domain.Plugin;

namespace Workflow.Application.Designer.Interfaces;

public interface IWorkflowDesigner
{
    void AddPlugin(PluginTypeId pluginTypeId, PluginId pluginId);
    void RemovePlugin(PluginId pluginId);
    IPluginConfig GetPluginConfig(PluginId pluginId);
    void SetPluginConfig(PluginId pluginId, IPluginConfig config);
    void AddLink(LinkId LinkId, AddLinkConfig config);
    void RemoveLink(LinkId LinkId);
}