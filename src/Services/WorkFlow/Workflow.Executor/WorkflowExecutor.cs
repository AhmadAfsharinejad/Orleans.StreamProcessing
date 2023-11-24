using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Workflow.Domain;
using Workflow.Executor.Domain;
using Workflow.Infrastructure.Executer.Interfaces;

namespace Workflow.Executor;

//TODO Handle Parallelism -- mabey using actor?
internal sealed class WorkflowExecutor : IWorkflowExecutor
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<ExecutorConfig> _options;

    public WorkflowExecutor(IHttpClientFactory httpClientFactory, IOptions<ExecutorConfig> options)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    public async Task Run(WorkflowDesign workflowDesign)
    {
        var client = GetClient();
        await client.PostAsync("Run", JsonContent.Create(workflowDesign));
    }

    public async Task Stop(WorkflowId workflowId)
    {
        var client = GetClient();
        await client.PutAsync("Stop", JsonContent.Create(workflowId));
    }

    private HttpClient GetClient()
    {
        var client = _httpClientFactory.CreateClient("Executor");
        client.BaseAddress = new Uri(_options.Value.Url);
        return client;
    }
}