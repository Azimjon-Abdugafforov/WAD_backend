using Microsoft.EntityFrameworkCore;

namespace WAD.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Actors> Actors { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
    }
}
