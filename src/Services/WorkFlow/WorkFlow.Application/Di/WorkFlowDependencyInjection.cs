using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Application.Di;

public static class WorkflowDependencyInjection
{
    public static IServiceCollection AddWorkflowApplicationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddStreamServices();
        serviceCollection.AddMediator();
        
        return serviceCollection;
    }
    
    private static IServiceCollection AddStreamServices(this IServiceCollection serviceCollection)
    {
        var serviceAdders = FindDependencyIntroducers();
        foreach (var serviceAdder in serviceAdders)
        {
            serviceAdder.AddService(serviceCollection);
        }
        
        return serviceCollection;
    }
    
    private static IEnumerable<IServiceAdder> FindDependencyIntroducers()
    {
        var dependencyTypes = typeof(IServiceAdder).Assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           typeof(IServiceAdder).IsAssignableFrom(type))
            .ToArray();

        return dependencyTypes
            .Select(Activator.CreateInstance)
            .Cast<IServiceAdder>()
            .ToArray();
    }
}