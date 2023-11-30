using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreamProcessing.Sample;

var hostBuilder = new HostBuilder()
    .ConfigureHostConfiguration(configurationBinder => { configurationBinder.AddCommandLine(args); })
    .ConfigureAppConfiguration((ctx, configurationBinder) =>
    {
        var env = ctx.HostingEnvironment.EnvironmentName;
        configurationBinder.AddJsonFile("appsettings.json", true);
        configurationBinder.AddJsonFile($"appsettings.{env}.json", true);
        configurationBinder.AddCommandLine(args);
    })
    .UseOrleansClient((ctx, clientBuilder) =>
    {
        int hostGatewayInstanceId = ctx.Configuration.GetValue<int>("HostGatewayInstanceId");

        clientBuilder.UseLocalhostClustering(
            gatewayPort: 30000 + hostGatewayInstanceId
        );
    });

hostBuilder.UseConsoleLifetime();
hostBuilder.ConfigureServices(services => { services.AddHostedService<StartingHost>(); });

var host = hostBuilder.Build();
await host.StartAsync();

Console.WriteLine("Press enter to stop the Silo...");
Console.ReadLine();
await host.StopAsync();