using bacit_dotnet.MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bacit_dotnet.MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Teams> Teams { get; set; }

        public DbSet<Suggestions> Suggestions { get; set; }

        public DbSet<UsersTeams> UsersTeams { get; set; }
    }
}