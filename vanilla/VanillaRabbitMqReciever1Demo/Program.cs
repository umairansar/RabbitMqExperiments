using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Vanilla Reciever App 1";

IConnection connection = await factory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();

string exchangeName = "VanillaExchange";
string routingKey = "vanilla-routing-key";
string queueName = "VanillaQueue";

await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
await channel.QueueDeclareAsync(queueName, false, false, false, null);
await channel.QueueBindAsync(queueName, exchangeName, routingKey, null);
await channel.BasicQosAsync(0, 1, false);

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (sender, arg) => //Is the Event Handler
{
    // Task.Run(async () =>{ //Uncomment this makes it process multiple, only works for prefetchCount > 1
        var body = arg.Body.ToArray();
        string message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"B'Message recieved {message} before task on thread {Thread.CurrentThread.ManagedThreadId}.");
        await Task.Delay(8000);
        Console.WriteLine($"A'Message recieved {message} after task on thread {Thread.CurrentThread.ManagedThreadId}.");
        await channel.BasicAckAsync(arg.DeliveryTag, false); //Acks the message
    // });
};

string consumerTag = await channel.BasicConsumeAsync(queueName, false, consumer); //Initiates Consumption
await Task.Run(Console.ReadLine);
await channel.BasicCancelAsync(consumerTag);
await channel.CloseAsync();
await connection.CloseAsync();