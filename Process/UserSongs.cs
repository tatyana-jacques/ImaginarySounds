using System.ComponentModel.DataAnnotations.Schema;

namespace Process
{
    public class UserSongs
    {
        public int Id { get; set; }
        [ForeignKey("IdUser")]
        public int UserId { get; set; }
        [ForeignKey("IdSong")]
        public int SongId { get; set; }
       
    }
}
