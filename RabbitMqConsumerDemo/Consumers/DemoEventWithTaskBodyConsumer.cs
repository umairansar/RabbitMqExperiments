using MassTransit;
using RabbitMqDemo.Events;

namespace RabbitMqConsumerDemo.Consumers;

public class DemoEventWithTaskBodyConsumer : IConsumer<DemoEventWithTaskBody>
{
    private readonly ILogger<DemoEventWithTaskBodyConsumer> _logger;

    public DemoEventWithTaskBodyConsumer(ILogger<DemoEventWithTaskBodyConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DemoEventWithTaskBody> context)
    {
        _logger.LogInformation(
            "Received message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
        
        await Task.Run(async () =>
        {
            _logger.LogInformation(
                "Entered Task.Run. Message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
                context.Message,
                Thread.CurrentThread.ManagedThreadId, 
                Task.CurrentId ?? -1);
            
            // Simulate delay
            await Task.Delay(1000);
            
            _logger.LogInformation(
                "Exiting Task.Run. Message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
                context.Message,
                Thread.CurrentThread.ManagedThreadId, 
                Task.CurrentId ?? -1);
        });
        
        _logger.LogInformation(
            "Finished processing message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
    }
}