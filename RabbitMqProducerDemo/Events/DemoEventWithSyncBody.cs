namespace RabbitMqDemo.Events;

public record DemoEventWithSyncBody
{
    public string Id { get; init; }
    public DateTime Timestamp { get; init; }
}