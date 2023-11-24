using Mediator;
using Workflow.Domain;

namespace Workflow.Application.DesignCoordinator.Commands.RemoveLink;

public record struct RemoveLinkCommandConfig(WorkflowId WorkflowId, LinkId LinkId) : IRequest;
