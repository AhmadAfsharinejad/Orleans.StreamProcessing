using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using StreamProcessing.Scenario.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.DummyOutput;
using Workflow.Domain.Plugins.Filter;
using Workflow.Domain.Plugins.HttpListener;
using Workflow.Domain.Plugins.HttpResponse;
using Workflow.Domain.Plugins.KafkaSink;
using Workflow.Domain.Plugins.KafkaSource;
using Workflow.Domain.Plugins.Map;
using Workflow.Domain.Plugins.RandomGenerator;
using Workflow.Domain.Plugins.Rest;
using Workflow.Domain.Plugins.SqlExecutor;

// ReSharper disable UnusedMember.Local

namespace StreamProcessing.Sample;

internal sealed class StartingHost : BackgroundService
{
    private readonly IScenarioRunner _scenarioRunner;

    public StartingHost(IScenarioRunner scenarioRunner)
    {
        _scenarioRunner = scenarioRunner ?? throw new ArgumentNullException(nameof(scenarioRunner));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var sw = new Stopwatch();
        sw.Start();
        Console.WriteLine($"Start {DateTime.Now}");

        //await RunScenario();
        //await RunScenario_Http();
        //await RunScenario_Rest();
        await RunScenario_Map();
        //await RunScenario_Kafka();

        sw.Stop();
        Console.WriteLine($"Finished {DateTime.Now} {sw.Elapsed.TotalMilliseconds}");
    }

    private async Task RunScenario()
    {
        var config = GetScenarioConfig();
        await _scenarioRunner.Run(config);
    }

    private static WorkflowDesign GetScenarioConfig()
    {
        var plugins = new List<Plugin>();
        var links = new List<Link>();

        var randomPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig);

        var randomPluginConfig2 = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig2);

        var randomPluginConfig3 = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig3);

        var randomPluginConfig4 = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig4);

        var filterPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.Filter), new PluginId(Guid.NewGuid()), GetFilterConfig());
        plugins.Add(filterPluginConfig);

        var filterPluginConfig2 = new Plugin(new PluginTypeId(PluginTypeNames.Filter), new PluginId(Guid.NewGuid()), GetFilterConfig());
        plugins.Add(filterPluginConfig2);

        var sqlExecutorConfig = new Plugin(new PluginTypeId(PluginTypeNames.SqlExecutor), new PluginId(Guid.NewGuid()), GetSqlExecutorConfig());
        plugins.Add(sqlExecutorConfig);

        var dummyOutputPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.DummyOutput), new PluginId(Guid.NewGuid()), GetDummyOutputConfig(10_000));
        plugins.Add(dummyOutputPluginConfig);

        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig.Id, new PortId()) ,  
            new PluginIdWithPort(filterPluginConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig2.Id, new PortId()) ,  
            new PluginIdWithPort(filterPluginConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig3.Id, new PortId()) ,  
            new PluginIdWithPort(filterPluginConfig2.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig4.Id, new PortId()) ,  
            new PluginIdWithPort(filterPluginConfig2.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(filterPluginConfig.Id, new PortId()) ,  
            new PluginIdWithPort(dummyOutputPluginConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(filterPluginConfig2.Id, new PortId()) ,  
            new PluginIdWithPort(sqlExecutorConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(sqlExecutorConfig.Id, new PortId()) ,  
            new PluginIdWithPort(dummyOutputPluginConfig.Id, new PortId())));

        return new WorkflowDesign( new WorkflowId(Guid.NewGuid()), new PluginAndLinks(plugins, links));
    }

    private async Task RunScenario_Http()
    {
        var config = GetScenarioConfig_Http();
        await _scenarioRunner.Run(config);
    }

    private static WorkflowDesign GetScenarioConfig_Http()
    {
        var plugins = new List<Plugin>();
        var links = new List<Link>();

        var httpPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.HttpListener), new PluginId(Guid.NewGuid()), GetHttpListenerConfig());
        plugins.Add(httpPluginConfig);

        var httpResponseConfig = new Plugin(new PluginTypeId(PluginTypeNames.HttpResponse), new PluginId(Guid.NewGuid()), GetHttpResponseConfig());
        plugins.Add(httpResponseConfig);

        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(httpPluginConfig.Id, new PortId()) ,  
            new PluginIdWithPort(httpResponseConfig.Id, new PortId())));

        return new WorkflowDesign(new WorkflowId(Guid.NewGuid()), new PluginAndLinks(plugins, links));
    }

    private async Task RunScenario_Rest()
    {
        var config = GetScenarioConfig_Rest();
        await _scenarioRunner.Run(config);
    }

    private static WorkflowDesign GetScenarioConfig_Rest()
    {
        var plugins = new List<Plugin>();
        var links = new List<Link>();

        var randomPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.Random),  new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig2());
        plugins.Add(randomPluginConfig);

        var restConfig = new Plugin(new PluginTypeId(PluginTypeNames.Rest), new PluginId(Guid.NewGuid()), GetRestConfig());
        plugins.Add(restConfig);

        var dummyOutputPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.DummyOutput), new PluginId(Guid.NewGuid()), GetDummyOutputConfig(10_000));
        plugins.Add(dummyOutputPluginConfig);

        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig.Id, new PortId()) ,  
            new PluginIdWithPort(restConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(restConfig.Id, new PortId()) ,  
            new PluginIdWithPort(dummyOutputPluginConfig.Id, new PortId())));

        return new WorkflowDesign(new WorkflowId(Guid.NewGuid()), new PluginAndLinks(plugins, links));
    }

    private async Task RunScenario_Map()
    {
        var config = GetScenarioConfig_Map();
        await _scenarioRunner.Run(config);
    }

    private static WorkflowDesign GetScenarioConfig_Map()
    {
        var plugins = new List<Plugin>();
        var links = new List<Link>();

        var randomPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig);
        
        var randomPluginConfig2 = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig2);
        
        var randomPluginConfig3 = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig3);
        
        var randomPluginConfig4 = new Plugin(new PluginTypeId(PluginTypeNames.Random), new PluginId(Guid.NewGuid()), GetRandomGeneratorConfig());
        plugins.Add(randomPluginConfig4);

        var mapConfig = new Plugin(new PluginTypeId(PluginTypeNames.Map), new PluginId(Guid.NewGuid()), GetMapConfig());
        plugins.Add(mapConfig);
        
        var dummyOutputPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.DummyOutput), new PluginId(Guid.NewGuid()), GetDummyOutputConfig(100_000));
        plugins.Add(dummyOutputPluginConfig);

        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig.Id, new PortId()) ,  
            new PluginIdWithPort(mapConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig2.Id, new PortId()) ,  
            new PluginIdWithPort(mapConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig3.Id, new PortId()) ,  
            new PluginIdWithPort(mapConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(randomPluginConfig4.Id, new PortId()) ,  
            new PluginIdWithPort(mapConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(mapConfig.Id, new PortId()) ,  
            new PluginIdWithPort(dummyOutputPluginConfig.Id, new PortId())));

        return new WorkflowDesign(new WorkflowId(Guid.NewGuid()), new PluginAndLinks(plugins, links));
    }

    private async Task RunScenario_Kafka()
    {
        var config = GetScenarioConfig_Kafka();
        await _scenarioRunner.Run(config);
    }

    private static WorkflowDesign GetScenarioConfig_Kafka()
    {
        var plugins = new List<Plugin>();
        var links = new List<Link>();

        var kafkaSourcePluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.KafkaSource), new PluginId(Guid.NewGuid()), GetKafkaSourceConfig());
        plugins.Add(kafkaSourcePluginConfig);

        var dummyOutputPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.DummyOutput), new PluginId(Guid.NewGuid()), GetDummyOutputConfig(10));
        plugins.Add(dummyOutputPluginConfig);

        var kafkaSinkPluginConfig = new Plugin(new PluginTypeId(PluginTypeNames.KafkaSink), new PluginId(Guid.NewGuid()), GetKafkaSinkConfig());
        plugins.Add(kafkaSinkPluginConfig);

        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(kafkaSourcePluginConfig.Id, new PortId()) ,  
            new PluginIdWithPort(dummyOutputPluginConfig.Id, new PortId())));
        links.Add(new Link(new LinkId(), 
            new PluginIdWithPort(kafkaSourcePluginConfig.Id, new PortId()) ,  
            new PluginIdWithPort(kafkaSinkPluginConfig.Id, new PortId())));

        return new WorkflowDesign(new WorkflowId(Guid.NewGuid()), new PluginAndLinks(plugins, links));
    }
    
    private static KafkaSinkConfig GetKafkaSinkConfig()
    {
        return new KafkaSinkConfig
        {
            BootstrapServers = "localhost:9092",
            Topic = "topic3",
            StaticMessageKeyFieldName ="k1", 
            MessageValueFieldName = "f1" 
        };
    }
    
    private static KafkaSourceConfig GetKafkaSourceConfig()
    {
        return new KafkaSourceConfig
        {
            BootstrapServers = "localhost:9092",
            Topic = "topic2",
            GroupId = "g1",
            OutputFieldName = "f1",
        };
    }
    
    private static RestConfig GetRestConfig()
    {
        return new RestConfig
        {
            JoinType = RecordJoinType.Append,
            Uri = "http://localhost:5089/Math/Add",
            HttpMethod = HttpMethod.Get,
            QueryStrings = new[] { new QueryStringField("b", "num") },
            StaticQueryStrings = new[] { new KeyValuePair<string, string>("a", "1") },
            RequestStaticHeaders = new[] { new KeyValuePair<string, string>("Accept", "application/json") },
            ResponseContentFieldName = "response",
            StatusFieldName = "status"
        };
    }

    private static HttpResponseConfig GetHttpResponseConfig()
    {
        return new HttpResponseConfig
        {
            Content = "By",
            StaticHeaders = new[] { new KeyValuePair<string, string>("id1", "1") },
            Headers = new[] { new HeaderField("resId", "fieldId") }
        };
    }

    private static HttpListenerConfig GetHttpListenerConfig()
    {
        return new HttpListenerConfig
        {
            Uri = "http://localhost:2185/index/",
            Headers = new[] { new HeaderField("id", "fieldId") }
        };
    }

    private static RandomGeneratorConfig GetRandomGeneratorConfig()
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

    private static RandomGeneratorConfig GetRandomGeneratorConfig2()
    {
        return new RandomGeneratorConfig
        {
            Columns = new List<RandomColumn>
            {
                new(new("num", FieldType.Integer), RandomType.Number)
            },
            Count = 100000,
            BatchCount = 10
        };
    }

    private static FilterConfig GetFilterConfig()
    {
        var constraints = new List<IConstraint>
        {
            new FieldConstraint
            {
                Operator = ConstraintOperators.Greater,
                FieldName = "Age",
                Value = 15
            },
            new FieldConstraint
            {
                Operator = ConstraintOperators.Less,
                FieldName = "Age",
                Value = 40
            }
        };

        return new FilterConfig
        {
            Constraint = new LogicalConstraint { Operator = ConstraintOperator.And, Constraints = constraints }
        };
    }

    private static SqlExecutorConfig GetSqlExecutorConfig()
    {
        return new SqlExecutorConfig
        {
            ConnectionString = @"Driver={ClickHouse ODBC Driver (Unicode)};Host=localhost;PORT=8123;Timeout=500;Username=admin;Password=admin",
            JoinType = RecordJoinType.Append,
            DqlCommand = new DqlCommand
            {
                CommandText = @"SELECT now() as dateTime, ? as age",
                ParameterFields = new[] { "Age" },
                OutputFields = new[]
                {
                    new DqlField("dateTime", new StreamField("db_dateTime", FieldType.DateTime)),
                    new DqlField("age", new StreamField("db_age", FieldType.Integer))
                }
            },
            DmlCommands = null
        };
    }

    private static DummyOutputConfig GetDummyOutputConfig(int recordCountInterval)
    {
        return new DummyOutputConfig
        {
            IsWriteEnabled = true,
            RecordCountInterval = recordCountInterval
        };
    }

    private static MapConfig GetMapConfig()
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
            Code = @"using System.Collections.Generic;
namespace MapNameSpace;
public class MapClass
{
    public static IReadOnlyDictionary<string, object> Map(IReadOnlyDictionary<string, object> input)
    {
        var output = new Dictionary<string, object>(input);
        output[""FullName""] = input[""Name""].ToString() + "" ""+ input[""LastName""].ToString();
        output[""IsChild""] = (int)input[""Age""] < 13;
        return output;
    }
}"
        };
    }
}