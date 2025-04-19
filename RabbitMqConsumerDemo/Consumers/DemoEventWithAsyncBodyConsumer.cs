using MassTransit;
using RabbitMqDemo.Events;

namespace RabbitMqConsumerDemo.Consumers;

public class DemoEventWithAsyncBodyConsumer : IConsumer<DemoEventWithAsyncBody>
{
    private readonly ILogger<DemoEventWithAsyncBodyConsumer> _logger;

    public DemoEventWithAsyncBodyConsumer(ILogger<DemoEventWithAsyncBodyConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<DemoEventWithAsyncBody> context)
    {
        _logger.LogInformation(
            "Received message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
        
        await Task.Delay(2000); 
        
        _logger.LogInformation(
            "Finished processing message: {Message}. Thread ID: {ThreadId}, Task ID: {TaskId}", 
            context.Message,
            Thread.CurrentThread.ManagedThreadId, 
            Task.CurrentId ?? -1);
    }
}