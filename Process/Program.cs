using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using System.Text;
using Process;

var factory = new ConnectionFactory()
{
    HostName = "167.172.186.10",
    UserName = "tatyana",
    Password = "learningRabbitMQ"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();


    channel.QueueDeclare(queue: "addToLib",
                               durable: true,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var userSongs = JsonConvert.DeserializeObject<UserSongsViewModel>(message);
    using var ctx = new ProcessContext();
    var idUser = 0;

    foreach (var x in userSongs.UserSongList)
    {
        if (idUser == 0)
        {
            idUser = x.UserId;
        }
        var newEntity = new UserSongs
        {
            UserId = x.UserId,
            SongId = x.SongId
        };

        ctx.UserSongs.Add(newEntity);
    }

    var statusTable = ctx.StatusTable.Last(e => e.UserId == idUser);
    statusTable.Status = 1;
    ctx.Entry(statusTable).State = EntityState.Modified;
    ctx.SaveChangesAsync();


};


channel.BasicConsume(queue: "addToLib",
                        autoAck: true,
                        consumer: consumer);





