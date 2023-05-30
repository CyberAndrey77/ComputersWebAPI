using System.ComponentModel.DataAnnotations;

namespace Computers.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MinLength(1)]
        [MaxLength(50)]
        public string Login { get; set; }
        [MinLength(1)]
        [MaxLength(50)]
        public byte[] Password { get; set; }
        [MinLength(1)]
        [MaxLength(50)]
        public string Role { get; set; }
    }
}
