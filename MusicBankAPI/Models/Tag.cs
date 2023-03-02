using System.ComponentModel.DataAnnotations;

namespace MusicBankAPI.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Title { get; set; }
    }
}
