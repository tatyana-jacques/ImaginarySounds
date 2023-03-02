using System.ComponentModel.DataAnnotations;

namespace MusicBankAPI.Models
{
    public class ComposerViewModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
       
    }
}
