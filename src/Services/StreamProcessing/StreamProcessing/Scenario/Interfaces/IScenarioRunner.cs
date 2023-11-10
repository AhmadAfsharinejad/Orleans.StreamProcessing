using Workflow.Domain;

namespace StreamProcessing.Scenario.Interfaces;

public interface IScenarioRunner
{
    Task Run(WorkflowDesign config);
}