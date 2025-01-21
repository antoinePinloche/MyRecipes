using Microsoft.EntityFrameworkCore;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Recipes.Repository.EF.DbContext;

namespace MyRecipes.Recipes.Repository.EF.Repository
{
    public class EFRecipeIngredientRepository : RecipeIngredientBase
    {
        public RecipeDbContext Context { get; set; }

        public EFRecipeIngredientRepository(RecipeDbContext context) => Context = context;
        public override Task<RecipeIngredient> AddAsync(RecipeIngredient entity)
        {
            throw new NotImplementedException();
        }

        public override Task<RecipeIngredient> AddRangeAsync(ICollection<RecipeIngredient> entities)
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

        public override RecipeIngredient FirstOrDefault(Func<RecipeIngredient, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<RecipeIngredient>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<RecipeIngredient> GetAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveAsync(RecipeIngredient entitie)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeAsync(ICollection<RecipeIngredient> entities)
        {
            throw new NotImplementedException();
        }

        public override Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(RecipeIngredient entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateRangeAsync(ICollection<RecipeIngredient> entities)
        {
            throw new NotImplementedException();
        }
    }
}
