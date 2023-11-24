// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using Workflow.Domain;
using Workflow.Domain.Plugins;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.DummyOutput;
using Workflow.Domain.Plugins.Map;
using Workflow.Domain.Plugins.RandomGenerator;

var url = args.FirstOrDefault() ?? @"http://localhost:5078";

using var client = new HttpClient();
client.BaseAddress = new Uri(url);


var workflowId = Guid.NewGuid();
await client.PostAsync("/Workflow/CreateWorkflow", JsonContent.Create(workflowId));


var randomPluginId = new PluginId(Guid.NewGuid());
await client.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(randomPluginId, new PluginTypeId(PluginTypeNames.Random))));

var randomPluginIdWithConfig = new PluginIdWithConfig(randomPluginId, GetRandomGeneratorConfig());
await client.PutAsync($"/Workflow/SetPluginConfig/{workflowId}", GetStringContent(randomPluginIdWithConfig));


var mapPluginId = new PluginId(Guid.NewGuid());
await client.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(mapPluginId, new PluginTypeId(PluginTypeNames.Map))));


var randomToMapLink = new Link(new LinkId(Guid.NewGuid()), new PluginIdWithPort(randomPluginId, new PortId("p1")), new PluginIdWithPort(mapPluginId, new PortId("p1")));
await client.PostAsJsonAsync($"/Workflow/AddLink/{workflowId}", randomToMapLink);


var mapPluginIdWithConfig = new PluginIdWithConfig(mapPluginId, GetMapConfig());
await client.PutAsync($"/Workflow/SetPluginConfig/{workflowId}", GetStringContent(mapPluginIdWithConfig));


var dummyOutputPluginId = new PluginId(Guid.NewGuid());
await client.PostAsync($"/Workflow/AddPlugin/{workflowId}", JsonContent.Create(new PluginIdWithTypeId(dummyOutputPluginId, new PluginTypeId(PluginTypeNames.DummyOutput))));


var mapToOutputLink = new Link(new LinkId(Guid.NewGuid()), new PluginIdWithPort(mapPluginId, new PortId("p1")), new PluginIdWithPort(dummyOutputPluginId, new PortId("p1")));
await client.PostAsJsonAsync($"/Workflow/AddLink/{workflowId}", mapToOutputLink);


var dummyOutputPluginIdWithConfig = new PluginIdWithConfig(dummyOutputPluginId, GetDummyOutputConfig(100_000));
await client.PutAsync($"/Workflow/SetPluginConfig/{workflowId}", GetStringContent(dummyOutputPluginIdWithConfig));


await client.PostAsync($"/Workflow/Run/{workflowId}", null);


Console.WriteLine("Press any key to stop");
Console.ReadKey();


StringContent GetStringContent(object value)
{
    var serializeObject = JsonConvert.SerializeObject(value, new JsonSerializerSettings
    {
        TypeNameHandling = TypeNameHandling.All,
    });

    return new StringContent(serializeObject, Encoding.UTF8, "application/json");
}


RandomGeneratorConfig GetRandomGeneratorConfig()
{
    return new RandomGeneratorConfig
    {
        Columns = new List<RandomColumn>
        {
            new(new("Name", FieldType.Text), RandomType.Name),
            new(new("Age", FieldType.Integer), RandomType.Age),
            new(new("LastName", FieldType.Text), RandomType.LastName),
            new(new("DateTime", FieldType.DateTime), RandomType.DateTime)
        },
        Count = 1_000_000,
        BatchCount = 10
    };
}

MapConfig GetMapConfig()
{
    return new MapConfig
    {
        OutputColumns = new[]
        {
            new StreamField("Name", FieldType.Text),
            new StreamField("Age", FieldType.Integer),
            new StreamField("LastName", FieldType.Text),
            new StreamField("FullName", FieldType.Text),
            new StreamField("DateTime", FieldType.DateTime),
            new StreamField("IsChild", FieldType.Bool)
        },
        FunctionName = "Map",
        FullClassName = "MapNameSpace.MapClass",
        Code = """
using System.Collections.Generic;
namespace MapNameSpace;
public class MapClass
{
    public static IReadOnlyDictionary<string, object> Map(IReadOnlyDictionary<string, object> input)
    {
        var output = new Dictionary<string, object>(input);
        output["FullName"] = input["Name"].ToString() + " "+ input["LastName"].ToString();
        output["IsChild"] = (int)input["Age"] < 13;
        return output;
    }
}
"""
    };
}

DummyOutputConfig GetDummyOutputConfig(int recordCountInterval)
{
    return new DummyOutputConfig
    {
        IsWriteEnabled = true,
        RecordCountInterval = recordCountInterval
    };
}