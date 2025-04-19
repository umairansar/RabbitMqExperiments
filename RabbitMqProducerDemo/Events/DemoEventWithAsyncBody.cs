namespace RabbitMqDemo.Events;

public record DemoEventWithAsyncBody
{
    public string Id { get; init; }
    public DateTime Timestamp { get; init; }
}