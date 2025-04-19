// See https://aka.ms/new-console-template for more information

using MassTransit;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

var builder = Host.CreateApplicationBuilder(args);

using var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
});
var logger = loggerFactory.CreateLogger("MainProgram");
logger.LogInformation("Main Program (Async) - Thread ID: {ThreadId}, Task ID: {TaskId}", 
    Thread.CurrentThread.ManagedThreadId, 
    Task.CurrentId ?? -1);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.SetInMemorySagaRepositoryProvider();

    var assembly = typeof(Program).Assembly;
    x.AddConsumers(assembly);
    x.AddSagaStateMachines(assembly);
    x.AddSagas(assembly);
    x.AddActivities(assembly);
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

logger.LogInformation("Main Program (Async) - Thread ID: {ThreadId}, Task ID: {TaskId}", 
    Thread.CurrentThread.ManagedThreadId, 
    Task.CurrentId ?? -1);

app.Run();