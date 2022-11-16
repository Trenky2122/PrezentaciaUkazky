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
string queue1 = "queue1";
string queue2 = "queue2";
channel.QueueDeclare(queue: queue1,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
channel.QueueDeclare(queue: queue2,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
string exchange = "prednaskaExchange";
channel.ExchangeDeclare(exchange: exchange, type: "direct", durable: false, autoDelete: false, arguments: null);
channel.QueueBind(queue1, exchange, "1");
channel.QueueBind(queue2, exchange, "2");


string message = "message1!";
var body = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: exchange,
                     routingKey: "1",
                     basicProperties: null,
                     body: body);
message = "message2";
body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange: exchange,
                     routingKey: "2",
                     basicProperties: null,
                     body: body);
Console.WriteLine(" [x] Sent {0}", message);