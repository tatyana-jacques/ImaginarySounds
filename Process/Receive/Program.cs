using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Newtonsoft.Json;
using Send.Models;
using Receive;

var factory = new ConnectionFactory()
{
    HostName = "167.172.186.10",
    UserName = "tatyana",
    Password = "learningRabbitMQ"
};
using (var context = new UserReceiveContext())
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())

{

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        User user = JsonConvert.DeserializeObject<User>(message);
        context.Users.Add(user);
        context.SaveChangesAsync();

        Console.WriteLine($"{user.Name}; {user.Email}");
    };



    channel.BasicConsume(queue: "userRegistration",
                            autoAck: true,
                            consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}