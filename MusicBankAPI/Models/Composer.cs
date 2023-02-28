using System.ComponentModel.DataAnnotations;

namespace MusicBankAPI.Models
{
    public class Composer
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public List <Song> Songs { get; set; }
    }
}
