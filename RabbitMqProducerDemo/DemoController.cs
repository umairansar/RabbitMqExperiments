using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMqDemo.Events;

namespace RabbitMqDemo;

[ApiController]
[Route("api/v1/[controller]")]
public class DemoController : ControllerBase
{
    private IBus _bus;
    private int _counter = 1;
    private ILogger<DemoController> _logger;

    public DemoController(IBus bus, ILogger<DemoController> logger)
    {
        _bus = bus;
        _logger = logger;
    }
    
    [HttpGet("SendSyncBodyEvent/{eventCount}")]
    public async Task<IActionResult> SendSyncBodyEvent(int eventCount)
    {
        for (int i = 0; i < eventCount; i++)
        {
            _counter++;
            var eventMessage = new DemoEventWithSyncBody
            {
                Id = _counter.ToString(),
                Timestamp = DateTime.UtcNow
            };

            await _bus.Publish(eventMessage);
        }
        return Ok(new { Message = "Event published successfully", Id = _counter });
    }
    
    [HttpGet("SendAsyncBodyEvent/{eventCount}")]
    public async Task<IActionResult> SendAsyncBodyEvent(int eventCount)
    {
        for (int i = 0; i < eventCount; i++)
        {
            _counter++;
            var eventMessage = new DemoEventWithAsyncBody
            {
                Id = _counter.ToString(),
                Timestamp = DateTime.UtcNow
            };

            await _bus.Publish(eventMessage);
        }
        return Ok(new { Message = "Event published successfully", Id = _counter });
    }
    
    [HttpGet("SendTaskBodyEvent/{eventCount}")]
    public async Task<IActionResult> SendTaskBodyEvent(int eventCount)
    {
        for (int i = 0; i < eventCount; i++)
        {
            _counter++;
            var eventMessage = new DemoEventWithTaskBody
            {
                Id = _counter.ToString(),
                Timestamp = DateTime.UtcNow
            };

            await _bus.Publish(eventMessage);
        }
        return Ok(new { Message = "Event published successfully", Id = _counter });
    }
    
    [HttpGet("SendTaskInTaskBodyEvent/{eventCount}")]
    public async Task<IActionResult> SendTaskInTaskBodyEvent(int eventCount)
    {
        for (int i = 0; i < eventCount; i++)
        {
            _counter++;
            var eventMessage = new DemoEventWithTaskInTaskBody
            {
                Id = _counter.ToString(),
                Timestamp = DateTime.UtcNow
            };

            await _bus.Publish(eventMessage);
        }
        return Ok(new { Message = "Event published successfully", Id = _counter });
    }
}