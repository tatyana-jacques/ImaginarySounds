using System.ComponentModel.DataAnnotations;

namespace MusicBankAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
