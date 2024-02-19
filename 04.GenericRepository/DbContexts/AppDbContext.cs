using _04.GenericRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Name = "Tom", Surname = "Jhonsons"},
                    new User { Id = 2, Name = "Bob", Surname = "Marley"},
                    new User { Id = 3, Name = "Sam", Surname = "Nower"}
            );
        }
    }
}
