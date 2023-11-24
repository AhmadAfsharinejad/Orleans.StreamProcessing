using Microsoft.Extensions.DependencyInjection;
using StreamProcessing.Common.Interfaces;
using StreamProcessing.Di;

namespace StreamProcessing.WorkFlow;

internal sealed class WorkFlowDiAdder : IServiceAdder
{
    public void AddService(IServiceCollection collection)
    {
        collection.AddSingleton<IWorkflowRunnerGrain, WorkflowRunnerGrain>();
    }
}