using Computers.Models;
using Microsoft.EntityFrameworkCore;

namespace Computers.Repository
{
    public class DataBase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DataBase(DbContextOptions<DataBase> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
