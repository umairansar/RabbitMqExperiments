namespace RabbitMqDemo.Events;

public record DemoEventWithTaskInTaskBody
{
    public string Id { get; init; }
    public DateTime Timestamp { get; init; }
}