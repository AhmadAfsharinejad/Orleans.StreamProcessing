using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Di;
using Workflow.Application.PluginFinder.Interfaces;

namespace Workflow.Application.PluginFinder;

internal sealed class PluginConfigFinderDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IPluginConfigFinder, PluginConfigFinder>();
    }
}