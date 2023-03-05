using AutoMapper;
using MusicBankAPI.Models;
using MusicBankAPI.ViewModels;

namespace MusicBankAPI
{
    public class MusicBankProfile: Profile
    {
        public MusicBankProfile()
        {
            CreateMap<ArtistViewModel,Artist>();
            CreateMap<ComposerViewModel,Composer>();
            CreateMap<SongViewModel,Song>();
            CreateMap<SongTagsViewModel,SongTags>();
            CreateMap<TagViewModel,Tag>();
            CreateMap<UserViewModel,User>();
            CreateMap<UserSongViewModel,UserSongs>();
        }
    }
}
