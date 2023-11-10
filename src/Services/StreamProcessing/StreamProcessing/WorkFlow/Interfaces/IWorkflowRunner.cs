using Workflow.Domain;

namespace StreamProcessing.WorkFlow.Interfaces;

public interface IWorkflowRunner
{
    Task Run(WorkflowDesign config);
}