using Microsoft.EntityFrameworkCore;

namespace Expert.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Concert> Concerts { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
