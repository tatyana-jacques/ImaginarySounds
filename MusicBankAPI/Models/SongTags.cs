using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.Models
{
    public class SongTags
    {
        public int Id { get; set; }
        [ForeignKey("IdUser")]
        public int SongId { get; set; }
        [ForeignKey("IdSong")]
        public int TagId { get; set; }
        public Song Song { get; set; }
        public Tag Tag { get; set; }
    }
}
