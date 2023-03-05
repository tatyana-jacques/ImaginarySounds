using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Newtonsoft.Json;
using MusicBankAPI.Models;
using Receive;
using Process;

var factory = new ConnectionFactory()
{
    HostName = "167.172.186.10",
    UserName = "tatyana",
    Password = "learningRabbitMQ"
};
using (var ctx = new UserReceiveContext())
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())

{

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        UserSongsViewModel userSongs = JsonConvert.DeserializeObject<UserSongsViewModel>(message);

        var userSongsToList = ctx.UserSongs.ToList();
        var filteredList = FilterList(userSongsToList, userSongs.UserSongList);

        var idUser = 0;

        foreach (var x in filteredList)
        {
            if (idUser == 0)
            {
                idUser = x.UserId;
            }

            //var newEntity = CreateUserSongs(x.UserId, x.SongId);

            ctx.UserSongs.Add(x
            );

            Console.WriteLine($"{x.SongId}; {x.UserId}");
        }

        var statusTableToList = ctx.StatusTable.ToList();
        foreach (var x in statusTableToList)
        {
            Console.WriteLine(x.UserId);
        }
        var statusTable = statusTableToList.Last(e => e.UserId == idUser);
        Console.WriteLine("Aqui: " + statusTable.UserId);
        statusTable.Status = 1;
        ctx.StatusTable.Update(statusTable);
        ctx.SaveChangesAsync();

    };


    channel.BasicConsume(queue: "addToLib",
                            autoAck: true,
                            consumer: consumer);

    Console.WriteLine(" Press [enter] to exit.");
    Console.ReadLine();
}

UserSongs CreateUserSongs(int userId, int songId)
{
    return new UserSongs
    {
        UserId = userId,
        SongId = songId
    };

}

List<UserSongs> FilterList(List<UserSongs> contextList, List<UserSongViewModel> cartList)
{
    var returnList = new List<UserSongs>();

    foreach (var x in cartList)
    {
        bool contains = false;
        foreach (var y in contextList)
            if (y.UserId == x.UserId && y.SongId == x.SongId)
            {
                contains = true;

            }
        if (contains == false)
        {
            var entity = new UserSongs
            {
                UserId = x.UserId,
                SongId = x.SongId

            };
            returnList.Add(entity);
        }

    }
    return returnList;
}

