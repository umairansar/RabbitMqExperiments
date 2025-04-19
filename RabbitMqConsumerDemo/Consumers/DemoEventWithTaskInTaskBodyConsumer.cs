using MassTransit;
using RabbitMqDemo.Events;

namespace RabbitMqConsumerDemo.Consumers;

public class DemoEventWithTaskInTaskBodyConsumer : IConsumer<DemoEventWithTaskInTaskBody>
{
    private readonly ILogger<DemoEventWithTaskInTaskBodyConsumer> _logger;

    public DemoEventWithTaskInTaskBodyConsumer(ILogger<DemoEventWithTaskInTaskBodyConsumer> logger)
    {
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<DemoEventWithTaskInTaskBody> context)
    {
        _logger.LogInformation(
            "Received message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
        
        await Task.Run(async () =>
        {
            _logger.LogInformation(
                "Inside Task.Run. Message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
                context.Message,
                Thread.CurrentThread.ManagedThreadId, 
                Task.CurrentId ?? -1);
            
            // Simulate delay
            await AnotherTask(context.Message.ToString());
        });
        
        _logger.LogInformation(
            "Finished processing message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
    }

    private Task AnotherTask(string? message)
    {
        _logger.LogInformation(
            "Inside Task^2. Message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
        
        return Task.CompletedTask;
    }
}