using Microsoft.EntityFrameworkCore;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Repository.EF.DbContext;

namespace MyRecipes.Recipes.Repository.EF.Repository
{
    public class EFRecipeRepository : RecipesBase
    {
        public RecipeDbContext Context { get; set; }

        public EFRecipeRepository(RecipeDbContext context) => Context = context;
        public override Task<Recipe> AddAsync(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Recipe> AddRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }

        public override async Task CreateOrUpdateSchemaAsync()
        {
            bool pendingMigration = (await Context.Database.GetPendingMigrationsAsync()).Any();
            if (pendingMigration)
            {
                await Context.Database.MigrateAsync();
            }
        }

        public override Recipe FirstOrDefault(Func<Recipe, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<Recipe>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Recipe> GetAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveAsync(Recipe entitie)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }

        public override Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(Recipe entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }
    }
}
