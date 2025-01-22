using Microsoft.EntityFrameworkCore;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Repository.EF.DbContext;

namespace MyRecipes.Recipes.Repository.EF.Repository
{
    public class EFIngredientRepository : IngredientBase
    {
        public RecipeDbContext Context { get; set; }

        public EFIngredientRepository(RecipeDbContext context) => Context = context;

        public override async Task<Ingredient> AddAsync(Ingredient entity)
        {
            var ingredientAdd = await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return ingredientAdd.Entity;
        }

        public override Task<Ingredient> AddRangeAsync(ICollection<Ingredient> entities)
        {
            throw new NotImplementedException();
        }

        public override Ingredient FirstOrDefault(Func<Ingredient, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<Ingredient>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Ingredient> GetAsync(Guid key)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveAsync(Ingredient entitie)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeAsync(ICollection<Ingredient> entities)
        {
            throw new NotImplementedException();
        }

        public override Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(Ingredient entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateRangeAsync(ICollection<Ingredient> entities)
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
    }
}
