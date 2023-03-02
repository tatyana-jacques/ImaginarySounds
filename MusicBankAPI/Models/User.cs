using System.ComponentModel.DataAnnotations;

namespace MusicBankAPI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Senha { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
