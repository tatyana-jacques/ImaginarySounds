using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicBankAPI.Models;
using Process;

namespace ReceiveUserSong
{
    public class FilterList
    {
        public List<UserSongs> List(List<UserSongs> contextList, List<UserSongViewModel> cartList)
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
    }
}