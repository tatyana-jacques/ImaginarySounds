using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.ViewModels
{
    public class SongViewModel
    {
        public string Title { get; set; }
       
        public string StorageData { get; set; }
        
        public string Cover { get; set; }
        
        public int ComposerId { get; set; }
     
        public int ArtistId { get; set; }
        
    }
}
