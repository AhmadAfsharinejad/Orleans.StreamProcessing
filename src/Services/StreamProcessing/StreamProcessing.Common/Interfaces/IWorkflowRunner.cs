using Workflow.Domain;

namespace StreamProcessing.Common.Interfaces;

public interface IWorkflowRunner
{
    Task Run(WorkflowDesign config);
}