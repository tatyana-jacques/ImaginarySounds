using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.ViewModels
{
    public class UserSongsViewModel
    {
        
        public int UserId { get; set; }
        public int SongId { get; set; }
       
    }
}
