using Mediator;
using Workflow.Application.Designer.Domain;

namespace Workflow.Application.Coordinator.Commands.CreateWorkflow;

public sealed record CreateWorkflowConfig(WorkflowId Id) : IRequest;
