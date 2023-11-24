using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Application.Di;

internal interface IServiceAdder
{
    void AddService(IServiceCollection serviceCollection);
}