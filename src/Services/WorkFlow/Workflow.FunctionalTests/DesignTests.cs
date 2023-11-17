using System.Net;
using System.Net.Http.Json;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using Workflow.Domain;
using Workflow.Domain.Plugins;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.RandomGenerator;
using Xunit;

namespace Workflow.FunctionalTests;

public class DesignTests : IClassFixture<WorkflowApiFixture>
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
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));

        // Act
        var response2 = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));

        // Assert
        response2.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task AddPlugin_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));

        // Act
        var response = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(Guid.NewGuid()), new PluginTypeId(PluginTypeNames.Random))));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RemovePlugin_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var pluginId = Guid.NewGuid();
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(pluginId), new PluginTypeId(PluginTypeNames.Random))));

        // Act
        var response = await _httpClient.PutAsync($"/Workflow/RemovePlugin/{workflowId}", JsonContent.Create(pluginId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetPluginConfig_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var pluginId = Guid.NewGuid();
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(pluginId), new PluginTypeId(PluginTypeNames.Random))));

        // Act
        var response = await _httpClient.GetAsync($"/Workflow/GetPluginConfig/{workflowId}/?pluginId={pluginId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task SetPluginConfig_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var pluginId = Guid.NewGuid();
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(pluginId), new PluginTypeId(PluginTypeNames.Random))));

        var config = new RandomGeneratorConfig { Count = 22 };
        var pluginIdWithConfig = new PluginIdWithConfig(new PluginId(pluginId), config);

        var serializeObject = JsonConvert.SerializeObject(pluginIdWithConfig, new JsonSerializerSettings ()
        {
            TypeNameHandling = TypeNameHandling.All,
        });
        var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        
        // Act
        var response = await _httpClient.PutAsync($"/Workflow/SetPluginConfig/{workflowId}", content);

        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetPluginConfig_ShouldReturnSettedConfig_WhenSetConfig()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var pluginId = Guid.NewGuid();
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(pluginId), new PluginTypeId(PluginTypeNames.Random))));

        var config = new RandomGeneratorConfig
        {
            Count = 1234,
            Columns = new[]
            {
                new RandomColumn(new StreamField("f1", FieldType.Float), RandomType.Age),
                new RandomColumn(new StreamField("f2", FieldType.Guid), RandomType.Guid)
            }
        };
        var pluginIdWithConfig = new PluginIdWithConfig(new PluginId(pluginId), config);
        var serializeObject = JsonConvert.SerializeObject(pluginIdWithConfig, new JsonSerializerSettings ()
        {
            TypeNameHandling = TypeNameHandling.All,
        });
        var content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
        var ___ = await _httpClient.PutAsync($"/Workflow/SetPluginConfig/{workflowId}", content);
        
        // Act
        var response = await _httpClient.GetAsync($"/Workflow/GetPluginConfig/{workflowId}/?pluginId={pluginId}");
        var body = await response.Content.ReadAsStringAsync();
        var actual = JsonConvert.DeserializeObject<RandomGeneratorConfig>(body);

        // Assert
        actual.Count.Should().Be(config.Count);
        actual.Columns.Should().BeEquivalentTo(config.Columns);
    }

    [Fact]
    public async Task AddLink_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));

        var sourceId = new PluginId(Guid.NewGuid());
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(sourceId, new PluginTypeId(PluginTypeNames.Random))));

        var targetId = new PluginId(Guid.NewGuid());
        var ___ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(targetId, new PluginTypeId(PluginTypeNames.DummyOutput))));

        var link = new Link(new LinkId(Guid.NewGuid()), new PluginIdWithPort(sourceId, new PortId("p1")), new PluginIdWithPort(targetId, new PortId("p1")));

        // Act
        var response = await _httpClient.PostAsJsonAsync($"/Workflow/AddLink/{workflowId}", link);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RemoveLink_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));

        var sourceId = new PluginId(Guid.NewGuid());
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(sourceId, new PluginTypeId(PluginTypeNames.Random))));

        var targetId = new PluginId(Guid.NewGuid());
        var ___ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(targetId, new PluginTypeId(PluginTypeNames.DummyOutput))));

        var linkId = Guid.NewGuid();
        var link = new Link(new LinkId(linkId), new PluginIdWithPort(sourceId, new PortId("p1")), new PluginIdWithPort(sourceId, new PortId("p1")));
        var ____ = await _httpClient.PostAsync($"/Workflow/AddLink/{workflowId}", JsonContent.Create(link));

        // Act
        var response = await _httpClient.PutAsync($"/Workflow/RemoveLink/{workflowId}", JsonContent.Create(linkId));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Run_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var pluginId = Guid.NewGuid();
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(pluginId), new PluginTypeId(PluginTypeNames.Random))));

        // Act
        var response = await _httpClient.PostAsync($"/Workflow/Run/{workflowId}", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Run_ShouldReturnError_WhenAlreadyIsRun()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var pluginId = Guid.NewGuid();
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(pluginId), new PluginTypeId(PluginTypeNames.Random))));
        var ___ = await _httpClient.PostAsync($"/Workflow/Run/{workflowId}", null);

        // Act
        var response = await _httpClient.PostAsync($"/Workflow/Run/{workflowId}", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task Stop_ShouldReturnOk_WhenAll()
    {
        // Arrange
        var workflowId = Guid.NewGuid();
        var _ = await _httpClient.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));
        var pluginId = Guid.NewGuid();
        var __ = await _httpClient.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(new PluginId(pluginId), new PluginTypeId(PluginTypeNames.Random))));
        var ___ = await _httpClient.PostAsync($"/Workflow/Run/{workflowId}", null);

        // Act
        var response = await _httpClient.PutAsync($"/Workflow/Stop/{workflowId}", null);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}