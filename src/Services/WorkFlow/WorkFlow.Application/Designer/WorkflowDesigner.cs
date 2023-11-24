using Workflow.Application.Designer.Exceptions;
using Workflow.Application.Designer.Interfaces;
using Workflow.Application.PluginFinder.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.Designer;

//TODO Handle Parallelism, Maybe using actor?
internal sealed class WorkflowDesigner : IWorkflowDesigner
{
    private readonly IPluginConfigFinder _pluginConfigFinder;
    private readonly IDictionary<PluginId, Plugin> _plugins;
    private readonly IDictionary<LinkId, Link> _links;

    public WorkflowDesigner(IPluginConfigFinder pluginConfigFinder)
    {
        _pluginConfigFinder = pluginConfigFinder ?? throw new ArgumentNullException(nameof(pluginConfigFinder));
        _plugins = new Dictionary<PluginId, Plugin>();
        _links = new Dictionary<LinkId, Link>();
    }

    public void AddPlugin(PluginTypeId pluginTypeId, PluginId pluginId)
    {
        if (_plugins.TryGetValue(pluginId, out _))
        {
            throw new DuplicateElementIdException(pluginId.Value.ToString());
        }

        var config = _pluginConfigFinder.GetConfig(pluginTypeId);
        _plugins.Add(pluginId, new Plugin(pluginTypeId, pluginId, config));
    }

    public void RemovePlugin(PluginId pluginId)
    {
        if (!_plugins.TryGetValue(pluginId, out _))
        {
            throw new NotExistsElementId(pluginId.Value.ToString());
        }

        _plugins.Remove(pluginId);
    }

    public IPluginConfig GetPluginConfig(PluginId pluginId)
    {
        if (!_plugins.TryGetValue(pluginId, out var plugin))
        {
            throw new NotExistsElementId(pluginId.Value.ToString());
        }

        return plugin.Config;
    }

    public void SetPluginConfig(PluginId pluginId, IPluginConfig config)
    {
        if (!_plugins.TryGetValue(pluginId, out var plugin))
        {
            throw new NotExistsElementId(pluginId.Value.ToString());
        }

        _plugins[pluginId] = plugin with { Config = config };
    }

    public void AddLink(Link config)
    {
        if (_links.TryGetValue(config.Id, out _))
        {
            throw new DuplicateElementIdException(config.Id.Value.ToString());
        }

        _links.Add(config.Id, config);
    }

    public void RemoveLink(LinkId linkId)
    {
        if (!_links.TryGetValue(linkId, out _))
        {
            throw new NotExistsElementId(linkId.Value.ToString());
        }

        _links.Remove(linkId);
    }

    public PluginAndLinks GetPluginAndLinks()
    {
        return new PluginAndLinks(_plugins.Values.ToList(), _links.Values.ToList());
    }
}