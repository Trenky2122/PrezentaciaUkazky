// See https://aka.ms/new-console-template for more information
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

string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "",
                     routingKey: "hello",
                     basicProperties: null,
                     body: body);
Console.WriteLine(" [x] Sent {0}", message);