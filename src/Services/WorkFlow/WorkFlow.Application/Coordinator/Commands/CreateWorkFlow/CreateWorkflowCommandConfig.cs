using Mediator;
using Workflow.Domain;

namespace Workflow.Application.Coordinator.Commands.CreateWorkflow;

public record struct CreateWorkflowCommandConfig(WorkflowId Id) : IRequest;
