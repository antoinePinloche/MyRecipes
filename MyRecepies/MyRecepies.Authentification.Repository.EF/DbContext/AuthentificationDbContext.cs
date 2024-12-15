
using Microsoft.EntityFrameworkCore;
using MyRecepies.Authentification.Domain.Entities;

namespace MyRecepies.Authentification.Repository.EF.DbContext
{
    public class AuthentificationDbContext : Microsoft.EntityFrameworkCore.DbContext
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
