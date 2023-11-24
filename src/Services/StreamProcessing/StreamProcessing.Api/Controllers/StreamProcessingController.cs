using Microsoft.AspNetCore.Mvc;
using StreamProcessing.Common.Domain;
using StreamProcessing.Common.Interfaces;
using Workflow.Domain;
#pragma warning disable CS4014

// ReSharper disable RouteTemplates.ParameterTypeAndConstraintsMismatch

namespace StreamProcessing.Api.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
public class StreamProcessingController : ControllerBase
{
    private readonly IClusterClient _clusterClient;

    public StreamProcessingController(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient ?? throw new ArgumentNullException(nameof(clusterClient));
    }

    [HttpPost]
    public async Task Run([FromBody] WorkflowDesign workflowDesign)
    {
        var grain = _clusterClient.GetGrain<IWorkflowRunnerGrain>(workflowDesign.Id.Value);
        grain.Run(new ImmutableWrapper<WorkflowDesign>(workflowDesign));
        await Task.CompletedTask;
    }

    [HttpPut]
    public async Task Stop([FromBody] WorkflowId workflowId)
    {
        var grain = _clusterClient.GetGrain<IWorkflowRunnerGrain>(workflowId.Value);
        await grain.Stop();
    }
}