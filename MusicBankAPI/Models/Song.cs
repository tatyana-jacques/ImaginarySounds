using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.Models
{
    public class SongViewModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(150)]
        public string StorageData { get; set; }
        [MaxLength(150)]
        public string Cover { get; set; }
        [ForeignKey("IdComposer")]
        public int ComposerId { get; set; }
        [ForeignKey("IdArtist")]
        public int ArtistId { get; set; }
        public ComposerViewModel Composer { get; set; }
        public ArtistViewModel Artist { get; set; }
    }
}
