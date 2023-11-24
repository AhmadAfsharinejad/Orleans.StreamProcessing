using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreamProcessing.Sample;


var hostBuilder = new HostBuilder()
    .UseOrleans((ctx, clientBuilder) =>
    {
        int hostGetWayId = ctx.Configuration.GetValue<int>("HostGetWayId");

        Console.WriteLine(hostGetWayId);
        
        clientBuilder.UseLocalhostClustering(
            //gatewayPort: 30000 + hostGetWayId
        );
    });

hostBuilder.UseConsoleLifetime();
hostBuilder.ConfigureServices(services => { services.AddHostedService<StartingHost>(); });

var host = hostBuilder.Build();
await host.StartAsync();

Console.WriteLine("Press enter to stop the Silo...");
Console.ReadLine();
await host.StopAsync();