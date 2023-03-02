using System.ComponentModel.DataAnnotations.Schema;

namespace MusicBankAPI.ViewModels
{
    public class SongTagsViewModel
    {
        public int SongId { get; set; }
        
        public int TagId { get; set; }
       
    }
}
