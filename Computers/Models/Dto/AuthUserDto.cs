namespace Computers.Models.Dto
{
    public class AuthUserDto
    {
        public Guid Guid { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public Role Role { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
