using Newtonsoft.Json;
using Workflow.Application.Di;
using Workflow.Executor;
using Workflow.Executor.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options => { options.SerializerSettings.TypeNameHandling = TypeNameHandling.All; });

builder.Services.Configure<ExecutorConfig>(builder.Configuration.GetSection("Executor"));

builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWorkflowApplicationServices();
builder.Services.AddExecutionServices();

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