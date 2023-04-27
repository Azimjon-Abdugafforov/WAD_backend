using Microsoft.EntityFrameworkCore;
using WAD.Models;

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
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<UserDto> UserDto { get; set; }
     



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageModel>().Property(i => i.Data).HasColumnType("image");
        }


        
    }
}
