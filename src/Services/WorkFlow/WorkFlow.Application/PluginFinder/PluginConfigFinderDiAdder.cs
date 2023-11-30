using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Di;
using Workflow.Application.PluginFinder.Domain;
using Workflow.Application.PluginFinder.Interfaces;
using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Application.PluginFinder;

internal sealed class PluginConfigFinderDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IPluginConfigFinder, PluginConfigFinder>();
        AddConfgis(serviceCollection);
    }

    private void AddConfgis(IServiceCollection serviceCollection)
    {
        var types =  typeof(ConfigAttribute).Assembly.GetTypes()
            .Where(type => type.GetCustomAttributes(typeof(ConfigAttribute), true).Length > 0);

        
        foreach (var type in types)
        {
            var attribute = (ConfigAttribute)type.GetCustomAttributes(typeof(ConfigAttribute), false).FirstOrDefault()!;

            serviceCollection.AddTransient<PluginConfigTypeWithId>(_ => new PluginConfigTypeWithId(attribute.PluginTypeId, type));
        }
    }
}