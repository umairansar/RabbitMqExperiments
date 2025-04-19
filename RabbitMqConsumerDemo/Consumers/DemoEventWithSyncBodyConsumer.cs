using MassTransit;
using RabbitMqDemo.Events;

namespace RabbitMqConsumerDemo.Consumers;

public class DemoEventWithSyncBodyConsumer : IConsumer<DemoEventWithSyncBody>
{
    private readonly ILogger<DemoEventWithSyncBodyConsumer> _logger;

    public DemoEventWithSyncBodyConsumer(ILogger<DemoEventWithSyncBodyConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<DemoEventWithSyncBody> context)
    {
        _logger.LogInformation(
            "Received message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
        
        // await Task.Delay(1000); 
        
        _logger.LogInformation(
            "Finished processing message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);

        return Task.CompletedTask;
    }
}