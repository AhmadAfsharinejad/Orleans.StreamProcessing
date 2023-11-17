using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Workflow.Application.Executer.Interfaces;
using Workflow.FunctionalTests.Mock;

// ReSharper disable ClassNeverInstantiated.Global

namespace Workflow.FunctionalTests;

public sealed class WorkflowApiFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var executorDescriptor = services.Single(
                d => d.ServiceType == typeof(IWorkflowExecutor));
            services.Remove(executorDescriptor);

            services.AddTransient<IWorkflowExecutor, MockWorkflowExecutor>();
        });
    }
}