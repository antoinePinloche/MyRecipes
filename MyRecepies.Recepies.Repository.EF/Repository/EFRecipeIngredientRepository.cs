using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Recipes.Repository.EF.DbContext;

namespace MyRecipes.Recipes.Repository.EF.Repository
{
    public class EFRecipeIngredientRepository : RecipeIngredientBase
    {
        public RecipeDbContext Context { get; set; }

        public EFRecipeIngredientRepository(RecipeDbContext context) => Context = context;
        public override async Task<RecipeIngredient> AddAsync(RecipeIngredient entity)
        {
            var entityAdd = await Context.RecipeIngredients.AddAsync(entity);
            await this.SaveAsync();
            return entityAdd.Entity;
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

        public override async Task<ICollection<RecipeIngredient>> GetAllAsync()
        {
            var entitiesFound = await Context.RecipeIngredients.Include(i => i.Ingredient).Include(i => i.Ingredient.FoodType).ToListAsync();
            return entitiesFound;
        }

        public override async Task<ICollection<RecipeIngredient>> GetAllRecipeIngredientByRecipeIdlAsync(Guid Key)
        {
            var entitiesFound = await Context.RecipeIngredients.Include(i => i.Ingredient)
                .Include(i => i.Ingredient.FoodType)
                .Where(w => w.RecipeId == Key)
                .ToListAsync();
            return entitiesFound;
        }
        public override async Task<RecipeIngredient> GetAsync(Guid key)
        {
            var entityFound = await Context.RecipeIngredients.Include(i => i.Ingredient).Include(i => i.Ingredient.FoodType).FirstOrDefaultAsync(f => f.Id == key);
            return entityFound;
        }

        public override async Task RemoveAsync(RecipeIngredient entitie)
        {
            if (entitie is not null)
            {
                Context.ChangeTracker.Clear();
                Context.RecipeIngredients.Remove(entitie);
                await this.SaveAsync();
            }
        }

        public override async Task RemoveRangeAsync(ICollection<RecipeIngredient> entities)
        {
            foreach(var entity in entities)
            {
                await this.RemoveAsync(entity);
            }
        }

        public override async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(RecipeIngredient entity)
        {
            Context.ChangeTracker.Clear();
            Context.RecipeIngredients.Update(entity);
            await this.SaveAsync();
        }

        public async override Task UpdateRangeAsync(ICollection<RecipeIngredient> entities)
        {
            foreach(var entity in entities)
            {
                await this.UpdateAsync(entity);
            }
        }
    }
}
