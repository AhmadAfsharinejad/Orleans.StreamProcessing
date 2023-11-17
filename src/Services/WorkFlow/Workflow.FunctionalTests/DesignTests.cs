using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Workflow.Api.Domain;
using Workflow.Domain;
using Workflow.Domain.Plugins;
using Xunit;

namespace Workflow.FunctionalTests;

public class DesignTests: IClassFixture<WorkflowApiFixture>
{
    private readonly HttpClient _httpClient;

    public DesignTests(WorkflowApiFixture fixture)
    {
        _httpClient = fixture.CreateClient();
    }
    
    [Fact]
    public async Task CreateWorkflow_ShouldReturnOk_WhenIdIsNew()
    {
        // Arrange
        var workflowId = Guid.NewGuid();

        // Act
        var response = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task CreateWorkflow_ShouldReturnError_WhenSameCreateWithDuplicatedId()
    {
        // Arrange
        var workflowId = Guid.NewGuid();

        // Act
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var response2 = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));

        // Assert
        response2.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
    
    [Fact]
    public async Task AddPlugin_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();

        // Act
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var response = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(Guid.NewGuid()), new PluginTypeId(PluginTypeNames.Filter))));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}