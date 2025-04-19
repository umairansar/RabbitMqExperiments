namespace RabbitMqDemo.Events;

public record DemoEventWithTaskBody
{
    public string Id { get; init; }
    public DateTime Timestamp { get; init; }
}