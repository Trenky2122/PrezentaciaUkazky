// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

string hostName = "dev-scorpion.pfs.local";
string virtualHost = "prednaska";
string userName = "prednaska";
string password = "00000000";
var factory = new ConnectionFactory()
{
    HostName = hostName,
    VirtualHost = virtualHost,
    UserName = userName,
    Password = password
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(queue: "hello",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" [x] Received {0}", message);
    Thread.Sleep(1000);
};
channel.BasicConsume(queue: "hello",
                        autoAck: true,
                        consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();