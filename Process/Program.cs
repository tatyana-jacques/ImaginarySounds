using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.EntityFrameworkCore;
using System;
using Newtonsoft.Json;
using System.Text;
using Process;
using MusicBankAPI.Models;

var factory = new ConnectionFactory()
{
    HostName = "167.172.186.10",
    UserName = "tatyana",
    Password = "learningRabbitMQ"
};

using (var ctx = new ProcessContext())
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{


    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        UserSongs userSongs = JsonConvert.DeserializeObject<UserSongs>(message);
        ctx.UserSongs.Add(userSongs);

        // foreach (var x in userSongs.UserSongList)
        // {
        //     // if (idUser == 0)
        //     // {
        //     //     idUser = x.UserId;
        //     // }
        //     var newEntity = new UserSongs
        //     {
        //         UserId = x.UserId,
        //         SongId = x.SongId
        //     };


        //     ctx.UserSongs.Add(newEntity);
        // }

        ctx.SaveChangesAsync();

        Console.WriteLine(userSongs.UserId);

    };


    channel.BasicConsume(queue: "addToLib",
                            autoAck: true,
                            consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
    // async Task SaveUserSongs(UserSongsViewModel userSongs)
    // {

    //     var idUser = 0;
    //     foreach (var x in userSongs.UserSongList)
    //     {
    //         if (idUser == 0)
    //         {
    //             idUser = x.UserId;
    //         }
    //         var newEntity = new UserSongs
    //         {
    //             UserId = x.UserId,
    //             SongId = x.SongId
    //         };
    //         Console.WriteLine("Hey " + newEntity.UserId + " " + newEntity.SongId + " " + idUser);

    //         ctx.UserSongs.Add(newEntity);
    //         await ctx.SaveChangesAsync();
    //     }

    //     var statusTable = ctx.StatusTable.Last(e => e.UserId == idUser);
    //     statusTable.Status = 1;
    //     ctx.Entry(statusTable).State = EntityState.Modified;

    //}
}

