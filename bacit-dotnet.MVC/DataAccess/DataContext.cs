using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#pragma warning disable

namespace bacit_dotnet.MVC.DataAccess
{
    // The DataContext is the source of all entities mapped over a database connection.
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<Suggestions> Suggestions { get; set; }

        public DbSet<Justdoit> Justdoit { get; set; }
        
        public DbSet<Teams> Teams { get; set; }
    }
}
