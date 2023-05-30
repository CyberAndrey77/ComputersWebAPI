using System.ComponentModel.DataAnnotations;

namespace Computers.Models
{
    public class Computer
    {
        [Key]
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
