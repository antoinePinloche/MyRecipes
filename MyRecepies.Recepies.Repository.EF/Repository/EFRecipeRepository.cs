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
        public override async Task<Recipe> AddAsync(Recipe entity)
        {
            var entityAdd = await Context.Recipes.AddAsync(entity);
            await this.SaveAsync();
            return entityAdd.Entity;
        }

        public override Task<List<Recipe>> AddRangeAsync(ICollection<Recipe> entities)
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

        public override async Task<ICollection<Recipe>> GetAllAsync()
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType).Include(i => i.Instructions).ToListAsync();
        }

        public async override Task<ICollection<Recipe>> GetByNameAsync(string Name)
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType)
                .Include(i => i.Instructions)
                .Where(w => w.Name.Contains(Name))
                .ToListAsync();
        }

        public override async Task<Recipe> GetAsync(Guid key)
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType).Include(i => i.Instructions).FirstOrDefaultAsync(f => f.Id == key);
        }

        public override async Task RemoveAsync(Recipe entitie)
        {
            Context.Recipes.Remove(entitie);
            await this.SaveAsync();
        }

        public override Task RemoveRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }

        public override async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public async override Task UpdateAsync(Recipe entity)
        {
            if (entity is not null)
            {
                Context.Recipes.Update(entity);
                await this.SaveAsync();
            }
        }

        public override Task UpdateRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }

        public async override Task<ICollection<Recipe>> GetByRecipeIdAsync(Guid recipeId)
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType)
                    .Include(i => i.Instructions)
                    .Where(w => w.UserId == recipeId)
                    .ToListAsync();
        }
    }
}
