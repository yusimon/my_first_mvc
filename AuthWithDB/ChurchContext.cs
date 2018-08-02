using AuthWithDB.Models;
using System.Data.Entity;

namespace AuthWithDB
{
    public class ChurchContext : DbContext
    {
        public ChurchContext() : base("name=DefaultConnection")
        {

        }

        // Church DbSet
        public DbSet<Activity> Activities { get; set; }

        public DbSet<ChurchUser> ChurchUsers { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
    }
}