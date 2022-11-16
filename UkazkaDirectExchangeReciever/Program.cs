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

var consumer1 = new EventingBasicConsumer(channel);
consumer1.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" [1] Received {0}", message);
};
channel.BasicConsume(queue: "queue1",
                        autoAck: true,
                        consumer: consumer1);
var consumer2 = new EventingBasicConsumer(channel);
consumer2.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" [2] Received {0}", message);
};
channel.BasicConsume(queue: "queue2",
                        autoAck: true,
                        consumer: consumer2);
Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();