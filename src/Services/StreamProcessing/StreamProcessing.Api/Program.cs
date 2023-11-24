using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Host.UseOrleansClient((ctx, clientBuilder) =>
{
    int hostGetWayId = ctx.Configuration.GetValue<int>("HostGetWayId");

    clientBuilder.UseLocalhostClustering(
        gatewayPort: 30000 + hostGetWayId
    );

    // clientBuilder.Configure<ClusterOptions>(options =>
    // {
    //     options.ClusterId = "ClusterId";
    //     options.ServiceId = "ServiceId";
    // });
    //  clientBuilder.UseRedisClustering(opt =>
    //  {
    //      opt.ConnectionString = "localhost:6379";
    //      opt.Database = 4;
    //  });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => { options.SerializerSettings.TypeNameHandling = TypeNameHandling.All; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseConsoleLifetime();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();