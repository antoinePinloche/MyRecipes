
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyRecipes.Authentification.Domain.Entities;

namespace MyRecipes.Authentification.Repository.EF.DbContext
{
    public class AuthentificationDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }

        public AuthentificationDbContext(DbContextOptions<AuthentificationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Authentification");
        }
    }
}
