using Mediator;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.AddLink;

public record struct AddLinkCommandConfig(WorkflowId WorkflowId, Link config) : IRequest;