using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Linq;
using System.Text;

var factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://hrpplbdi:EDX3pkhmXkX0xUZq4HuGcOA5ASB5Ykpu@whale.rmq.cloudamqp.com/hrpplbdi");

using var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare("hello", true, false, false);
Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    string message = $"Message {x}";
    var messageBody = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(string.Empty, "hello", null, messageBody);
    Console.WriteLine($"Message send: {message}");
});

Console.ReadLine();