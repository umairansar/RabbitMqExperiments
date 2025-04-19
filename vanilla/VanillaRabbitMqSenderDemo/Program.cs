using System.Text;
using RabbitMQ.Client;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Vanilla Sender App";

IConnection connection = await factory.CreateConnectionAsync();
IChannel channel1 = await connection.CreateChannelAsync();
IChannel channel2 = await connection.CreateChannelAsync();

string exchangeName = "VanillaExchange";
string routingKey = "vanilla-routing-key";
string queueName = "VanillaQueue";

await channel1.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
await channel1.QueueDeclareAsync(queueName, false, false, false, null);
await channel1.QueueBindAsync(queueName, exchangeName, routingKey, null);

await channel2.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
await channel2.QueueDeclareAsync(queueName, false, false, false, null);
await channel2.QueueBindAsync(queueName, exchangeName, routingKey, null);

byte[] messageBodyBytes1 = Encoding.UTF8.GetBytes("Hello Earth!");
await channel1.BasicPublishAsync(exchangeName, routingKey, false, messageBodyBytes1);

for (int i = 0; i < 20; i++)
{
    Console.WriteLine($"Sending message {i}.");
    byte[] messageBodyBytes2 = Encoding.UTF8.GetBytes($"Hello Heaven {i}!");
    await channel2.BasicPublishAsync(exchangeName, routingKey, false, messageBodyBytes2);
}

await Task.Run(Console.ReadLine);

await channel1.CloseAsync();
await connection.CloseAsync();







