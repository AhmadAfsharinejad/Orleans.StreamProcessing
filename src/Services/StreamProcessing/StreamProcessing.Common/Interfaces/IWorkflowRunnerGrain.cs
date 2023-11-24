using Orleans;
using StreamProcessing.Common.Domain;
using Workflow.Domain;

namespace StreamProcessing.Common.Interfaces;

public interface IWorkflowRunnerGrain : IGrainWithStringKey
{
    Task Run(ImmutableWrapper<WorkflowDesign> config);
}