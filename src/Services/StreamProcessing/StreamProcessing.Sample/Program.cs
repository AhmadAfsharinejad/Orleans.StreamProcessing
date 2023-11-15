using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreamProcessing.Di;
using StreamProcessing.Sample;
using StreamProcessing.Storage;


var host = CreateHost(12111, 30000).Build();
await host.StartAsync();


// var host2 = CreateHost(12112, 30001).Build();
// await host2.StartAsync();


Console.WriteLine("Press enter to stop the Silo...");
Console.ReadLine();
await host.StopAsync();

IHostBuilder CreateHost(int siloPort, int gatewayPort)
{
    var hostBuilder = new HostBuilder()
        .UseOrleans(siloBuilder =>
        {
            siloBuilder.UseLocalhostClustering();
            siloBuilder.AddMemoryGrainStorage(StorageConsts.StorageName);
            siloBuilder.AddStreamServices();
            siloBuilder.UseDashboard();
            // siloBuilder.ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole());
            //
            // siloBuilder.UseInMemoryReminderService();
            //
            //  siloBuilder.Configure<ClusterOptions>(options =>
            //  {
            //      options.ClusterId = "dev";
            //      options.ServiceId = "ServiceApp";
            //  });
            //  siloBuilder.UseRedisClustering(opt =>
            //  {
            //      opt.ConnectionString = "localhost:6379";
            //      opt.Database = 4;
            //  });
            //  siloBuilder.ConfigureEndpoints(siloPort: siloPort, gatewayPort: gatewayPort);
        });
    hostBuilder.ConfigureServices(services => { services.AddHostedService<StartingHost>(); });

    return hostBuilder;
}