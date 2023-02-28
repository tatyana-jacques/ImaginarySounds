using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Title { get; set; }
        [MaxLength(20)]
        public string Tag { get; set; }
        [MaxLength(10)]
        public string Tonality { get; set; }
        public int Duration { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string StorageData { get; set; }
        [MaxLength(100)]
        public string Cover { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        [ForeignKey("Composer")]
        public int ComposerId { get; set; }
        [ForeignKey("Artist")]
        public int ArtistId { get; set; }
        public Composer Composer { get; set; }
        public Artist Artist { get; set; }
        public List <User> Licenses {get; set; }



    }
}
