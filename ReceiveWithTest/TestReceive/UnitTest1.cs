using MusicBankAPI.Models;
using Process;
using ReceiveUserSong;

namespace TestReceive;

public class Tests
{
    FilterList filterList = new FilterList();

    List<UserSongs> userSongs1 = new List<UserSongs>{
       new UserSongs {
         UserId = 1,
         SongId = 1
        },
        new UserSongs {
         UserId = 1,
         SongId = 2
        },
        new UserSongs {
         UserId = 1,
         SongId = 3
        },
        new UserSongs {
         UserId = 2,
         SongId = 1
        },
        new UserSongs {
         UserId = 2,
         SongId = 2
        },
        new UserSongs {
         UserId = 2,
         SongId = 3
        }
    };
    List<UserSongs> userSongs2 = new List<UserSongs>{
        new UserSongs {
         UserId = 3,
         SongId = 1
        },
        new UserSongs {
         UserId = 3,
         SongId = 2
        },
        new UserSongs {
         UserId = 4,
         SongId = 3
        },
       new UserSongs {
         UserId = 4,
         SongId = 1
        },
        new UserSongs {
         UserId = 4,
         SongId = 2
        },
        new UserSongs {
         UserId = 3,
         SongId = 6
        }
    };

    List<UserSongViewModel> userSongViewModel1 = new List<UserSongViewModel>{
       new UserSongViewModel  {
         UserId = 1,
         SongId = 1
        },
        new UserSongViewModel {
         UserId = 1,
         SongId = 2
        },
        new UserSongViewModel {
         UserId = 1,
         SongId = 3
        },
        new UserSongViewModel {
         UserId = 2,
         SongId = 1
        },
        new UserSongViewModel {
         UserId = 2,
         SongId = 2
        },
        new UserSongViewModel {
         UserId = 2,
         SongId = 3
        }
    };
    List<UserSongViewModel> userSongViewModel2 = new List<UserSongViewModel>{
        new UserSongViewModel{
         UserId = 1,
         SongId = 1
        },
        new UserSongViewModel {
         UserId = 1,
         SongId = 2
        },
        new UserSongViewModel{
         UserId = 1,
         SongId = 3
        },
        new UserSongViewModel{
         UserId = 3,
         SongId = 1
        },
        new UserSongViewModel{
         UserId = 3,
         SongId = 2
        },
        new UserSongViewModel {
         UserId = 3,
         SongId = 3
        }
    };
    List<UserSongViewModel> userSongViewModel3 = new List<UserSongViewModel>{
        new UserSongViewModel{
         UserId = 1,
         SongId = 1
        },
        new UserSongViewModel {
         UserId = 1,
         SongId = 2
        },
        new UserSongViewModel{
         UserId = 5,
         SongId = 3
        },
        new UserSongViewModel{
         UserId = 5,
         SongId = 1
        },
        new UserSongViewModel{
         UserId = 3,
         SongId = 2
        },
        new UserSongViewModel {
         UserId = 3,
         SongId = 3
        }
    };


    [Test]
    public void TestFilterIsWorking1()
    {
        var testList = filterList.List(userSongs1, userSongViewModel1);
        Assert.AreEqual(testList.Count, 0);
    }

    [Test]
    public void TestFilterIsWorking2()
    {
        var testList = filterList.List(userSongs1, userSongViewModel2);
        Assert.AreEqual(testList.Count, 3);
    }

    [Test]
    public void TestFilterIsWorking3()
    {
        var testList = filterList.List(userSongs1, userSongViewModel3);
        Assert.AreEqual(testList.Count, 4);
    }

    [Test]
    public void TestFilterIsWorking4()
    {
        var testList = filterList.List(userSongs2, userSongViewModel1);
        Assert.AreEqual(testList.Count, 6);
    }

    [Test]
    public void TestFilterIsWorking5()
    {
        var testList = filterList.List(userSongs2, userSongViewModel2);
        Assert.AreEqual(testList.Count, 4);
    }

    [Test]
    public void TestFilterIsWorking6()
    {
        var testList = filterList.List(userSongs2, userSongViewModel3);
        Assert.AreEqual(testList.Count, 5);
    }




}