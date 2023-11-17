using Mediator;
using Microsoft.AspNetCore.Mvc;
using Workflow.Application.DesignCoordinator.Commands.AddLink;
using Workflow.Application.DesignCoordinator.Commands.AddPlugin;
using Workflow.Application.DesignCoordinator.Commands.CreateWorkFlow;
using Workflow.Application.DesignCoordinator.Commands.RemoveLink;
using Workflow.Application.DesignCoordinator.Commands.RemovePlugin;
using Workflow.Application.DesignCoordinator.Queries.GetPluginConfig;
using Workflow.Application.ExecuteCoordinator.Commands.Run;
using Workflow.Application.ExecuteCoordinator.Commands.Stop;
using Workflow.Domain;

// ReSharper disable RouteTemplates.ParameterTypeAndConstraintsMismatch

namespace Workflow.Api.Controllers;

[ApiController]
[Route("[controller]/[Action]/{workflowId:guid?}")]
public class WorkflowController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost]
    public async Task CreateWorkflow([FromBody] string id)
    {
        await _mediator.Send(new CreateWorkflowCommandConfig(new WorkflowId(id)));
    }

    [HttpPost]
    public async Task AddPlugin([FromRoute] WorkflowId workflowId, [FromBody] PluginIdWithTypeId pluginIdWithTypeId)
    {
        await _mediator.Send(new AddPluginCommandConfig(workflowId, pluginIdWithTypeId.TypeId, pluginIdWithTypeId.Id));
    }

    [HttpPut]
    public async Task RemovePlugin([FromRoute] WorkflowId workflowId, [FromBody] Guid pluginId)
    {
        await _mediator.Send(new RemovePluginCommandConfig(workflowId, new PluginId(pluginId)));
    }

    [HttpGet]
    public async Task<IPluginConfig> GetPluginConfig([FromRoute] WorkflowId workflowId, [FromQuery] Guid pluginId)
    {
        return await _mediator.Send(new GetPluginConfigCommandConfig(workflowId, new PluginId(pluginId)));
    }

    [HttpPost]
    public async Task AddLink([FromRoute] WorkflowId workflowId, [FromBody] Link link)
    {
        await _mediator.Send(new AddLinkCommandConfig(workflowId, link));
    }

    [HttpPut]
    public async Task RemoveLink([FromRoute] WorkflowId workflowId, [FromBody] Guid linkId)
    {
        await _mediator.Send(new RemoveLinkCommandConfig(workflowId, new LinkId(linkId)));
    }

    [HttpPost]
    public async Task Run([FromRoute] WorkflowId workflowId)
    {
        await _mediator.Send(new RunCommandConfig(workflowId));
    }

    [HttpPut]
    public async Task Stop([FromRoute] WorkflowId workflowId)
    {
        await _mediator.Send(new StopCommandConfig(workflowId));
    }
}