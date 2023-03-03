using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.Models
{
    public class UserSongs
    {
        public int Id { get; set; }
        [ForeignKey("IdUser")]
        public int UserId { get; set; }
        [ForeignKey("IdSong")]
        public int SongId { get; set; }
        public User User { get; set; }
        public Song Song { get; set; }
    }
}
