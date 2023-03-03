using System.ComponentModel.DataAnnotations;

namespace MusicBankAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Title { get; set; }
    }
}
