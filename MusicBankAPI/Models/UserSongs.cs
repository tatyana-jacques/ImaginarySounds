using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.Models
{
    public class UserSongsViewModel
    {
        public int Id { get; set; }
        [ForeignKey("IdUser")]
        public int UserId { get; set; }
        [ForeignKey("IdSong")]
        public int SongId { get; set; }
        public UserViewModel User { get; set; }
        public SongViewModel Song { get; set; }
    }
}
