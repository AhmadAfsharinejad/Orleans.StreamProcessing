// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
#pragma warning disable CS8321

const string BootstrapServers = "localhost:9092";
const string TopicName = "topic2";

var stopwatch = new Stopwatch();
stopwatch.Start();

//await CreateTopic();

//await Write();


var readTasks = GetPartitions().Select(Read);

await Task.WhenAll(readTasks);

stopwatch.Stop();
Console.WriteLine($"End {stopwatch.Elapsed.TotalMilliseconds}");
Console.ReadLine();

async Task Write()
{
    var config = new ProducerConfig
    {
        BootstrapServers = BootstrapServers
    };

    using var producer = new ProducerBuilder<string, string>(config).Build();

    var tasks = new List<Task>
    {
        //Produce(producer, 0),
        Produce(producer, 1),
        Produce(producer, 2),
        Produce(producer, 3),
        Produce(producer, 4)
    };

    await Task.WhenAll(tasks);
}

async Task Produce(IProducer<string, string> producer, int index)
{
    const int max = 100;
    var start = index * max;
    for (var i = start; i < start + max; i++)
    {
        await producer.ProduceAsync(TopicName,
            new Message<string, string> { Key = $"k{index}", Value = $"v{i}" });
    }
}

async Task Read(int partition)
{
    var config = new ConsumerConfig
    {
        BootstrapServers = BootstrapServers,
        GroupId = "g6",
        EnableAutoCommit = false,
        AutoOffsetReset = AutoOffsetReset.Earliest,
        EnablePartitionEof = true
    };

    using var consumer = new ConsumerBuilder<string, string>(config).Build();
    //consumer.Subscribe(new[] { "second-topic2" });
    consumer.Assign(new TopicPartitionOffset(TopicName, partition, Offset.Beginning));


    int count = 0;
    while (true)
    {
        var result = consumer.Consume();
        count++;
        //Console.WriteLine(result?.Message?.Value);
        if (result?.IsPartitionEOF ?? true) break;
    }

    Console.WriteLine($"Total: {count}");
}

async Task CreateTopic()
{
    using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = BootstrapServers }).Build();
    await adminClient.CreateTopicsAsync(new[]
    {
        new TopicSpecification { Name = TopicName, NumPartitions = 3 }
    });
}

IEnumerable<int> GetPartitions()
{
    using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = BootstrapServers }).Build();
    var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(20));

    var topic = meta.Topics.SingleOrDefault(t => t.Topic == TopicName);

    return topic!.Partitions.Select(x => x.PartitionId);
}