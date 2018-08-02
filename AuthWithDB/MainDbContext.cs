using AuthWithDB.Models;
using System.Data.Entity;

namespace AuthWithDB
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Lists> Lists { get; set; }

        public DbSet<NewUser> NewUsers { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Student> Students { get; set; }
        
        public DbSet<Person> People { get; set; }

        public DbSet<File> Files { get; set; }

        
    }
}