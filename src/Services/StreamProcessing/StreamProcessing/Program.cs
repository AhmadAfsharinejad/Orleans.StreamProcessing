﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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

        siloBuilder.AddActivityPropagation();
        
        siloBuilder.UseDashboard();
    });

hostBuilder.UseConsoleLifetime();

var host = hostBuilder.Build();

await host.StartAsync();

Console.WriteLine("Press enter to stop the Silo...");
Console.ReadLine();

await host.StopAsync();