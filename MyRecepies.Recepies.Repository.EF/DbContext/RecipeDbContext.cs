using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using MyRecipes.Recipes.Domain.Entity;
using System.Reflection.Metadata;

namespace MyRecipes.Recipes.Repository.EF.DbContext
{
    public class RecipeDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Recipe");

            modelBuilder.Entity<Ingredient>()
                .HasOne(e => e.FoodType);
            modelBuilder.Entity<Recipe>()
                .HasMany(e => e.Ingredients);
            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(e => e.Ingredient);
        }
    }
}
