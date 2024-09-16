using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Orleans.TestingHost;
using Serilog;
using StreamProcessing.Di;
using StreamProcessing.Storage;

var hostBuilder = new HostBuilder()
    .ConfigureHostConfiguration(configurationBinder => { configurationBinder.AddCommandLine(args); })
    .ConfigureAppConfiguration((ctx, configurationBinder) =>
    {
        var env = ctx.HostingEnvironment.EnvironmentName;
        configurationBinder.AddJsonFile("appsettings.json", true);
        configurationBinder.AddJsonFile($"appsettings.{env}.json", true);
        configurationBinder.AddCommandLine(args);
    })
    .UseOrleans((ctx, siloBuilder) =>
    {
        // siloBuilder.ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole());
        siloBuilder.AddMemoryGrainStorage(StorageConsts.StorageName);
        siloBuilder.AddStreamServices();

        int instanceId = ctx.Configuration.GetValue<int>("InstanceId");

        siloBuilder.UseLocalhostClustering(
            siloPort: 11111 + instanceId,
            gatewayPort: 30000 + instanceId
            //primarySiloEndpoint: new IPEndPoint(IPAddress.Loopback, 11111)
        );
        // siloBuilder.UseInMemoryReminderService();
        //
        //  siloBuilder.Configure<ClusterOptions>(options =>
        //  {
        //      options.ClusterId = "ClusterId";
        //      options.ServiceId = "ServiceId";
        //  });
        //  siloBuilder.UseRedisClustering(opt =>
        //  {
        //      opt.ConnectionString = "localhost:6379";
        //      opt.Database = 4;
        //  });
        //  siloBuilder.ConfigureEndpoints(siloPort: 11111 + instanceId, gatewayPort: 30000 + instanceId);


        // This is used for Telemetry
        // siloBuilder.AddActivityPropagation();


        siloBuilder.UseDashboard();
    });

hostBuilder.UseConsoleLifetime();

// Open Telemetry 
// hostBuilder.ConfigureServices(collection =>
// {
//     collection.AddOpenTelemetry()
//         .WithTracing(tracing =>
//         {
//             tracing.SetResourceBuilder(
//                 ResourceBuilder.CreateDefault()
//                     .AddService(serviceName: "StreamProcessing", serviceVersion: "1.0"));
//
//             tracing.AddSource("Microsoft.Orleans.Runtime");
//             tracing.AddSource("Microsoft.Orleans.Application");
//             
//             tracing.AddZipkinExporter(zipkin =>
//             {
//                 zipkin.Endpoint = new Uri("http://localhost:9411/api/v2/spans");
//             });
//         });
// });

// Logging Configuration

hostBuilder.UseSerilog((context, services, loggerConfiguration) =>
    loggerConfiguration
        .ReadFrom
        .Configuration(context.Configuration)
    );

var host = hostBuilder.Build();
await host.StartAsync();

Console.WriteLine("Press enter to stop the Silo...");
Console.ReadLine();

await host.StopAsync();