using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.PluginFinder.Domain;
using Workflow.Application.PluginFinder.Interfaces;
using Workflow.Domain;

namespace Workflow.Application.PluginFinder;

internal sealed class PluginConfigFinder : IPluginConfigFinder
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IReadOnlyDictionary<PluginTypeId, Type> _pluginTypesById;

    public PluginConfigFinder(IServiceProvider serviceProvider, IEnumerable<PluginConfigTypeWithId> plugins)
    {
        if (plugins == null)
        {
            throw new ArgumentNullException(nameof(plugins));
        }

        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        _pluginTypesById = plugins.ToDictionary(x => x.TypeId, z => z.ConfigType);
    }
    
    public IPluginConfig GetConfig(PluginTypeId pluginTypeId)
    {
        if (!_pluginTypesById.TryGetValue(pluginTypeId, out var configType))
        {
            throw new Exception($"Can't find plugin with id :'{pluginTypeId}'");
        }

        if (Activator.CreateInstance(configType) is not IPluginConfig config)
        {
            throw new Exception("Config must implement 'IPluginConfig'");
        }

        return config;
    }
}