using MassTransit;
using Microsoft.AspNetCore.Builder;
using RabbitMqDemo;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddHostedService<Worker>();
builder.Services.AddControllers(); // Required for controllers
builder.Services.AddEndpointsApiExplorer(); // Enables endpoint discovery for Swagger
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RabbitMqProducerDemo API V1");
    });
}

app.MapControllers(); // Required to map controller routes

app.Run();
