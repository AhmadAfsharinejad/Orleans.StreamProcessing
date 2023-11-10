using Workflow.Application.Designer.Domain;
using Workflow.Application.Designer.Interfaces;
using Workflow.Domain.Plugin;

namespace Workflow.Application.Designer;

//TODO
internal sealed class WorkflowDesigner : IWorkflowDesigner
{
    public void AddPlugin(PluginTypeId pluginTypeId, PluginId pluginId)
    {
        throw new NotImplementedException();
    }

    public void RemovePlugin(PluginId pluginId)
    {
        throw new NotImplementedException();
    }

    public IPluginConfig GetPluginConfig(PluginId pluginId)
    {
        throw new NotImplementedException();
    }

    public void SetPluginConfig(PluginId pluginId, IPluginConfig config)
    {
        throw new NotImplementedException();
    }

    public void AddLink(LinkId LinkId, AddLinkConfig config)
    {
        throw new NotImplementedException();
    }

    public void RemoveLink(LinkId LinkId)
    {
        throw new NotImplementedException();
    }
}