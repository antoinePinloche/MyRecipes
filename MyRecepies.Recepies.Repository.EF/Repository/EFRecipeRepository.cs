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
        /// <summary>
        /// <see cref="RecipesBase.AddAsync"/>
        /// </summary>
        public override async Task<Recipe> AddAsync(Recipe entity)
        {
            var entityAdd = await Context.Recipes.AddAsync(entity);
            await this.SaveAsync();
            return entityAdd.Entity;
        }
        /// <summary>
        /// <see cref="RecipesBase.AddRangeAsync"/>
        /// </summary>
        public override Task<ICollection<Recipe>> AddRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="RecipesBase.CreateOrUpdateSchemaAsync"/>
        /// </summary>
        public override async Task CreateOrUpdateSchemaAsync()
        {
            bool pendingMigration = (await Context.Database.GetPendingMigrationsAsync()).Any();
            if (pendingMigration)
            {
                await Context.Database.MigrateAsync();
            }
        }
        /// <summary>
        /// <see cref="RecipesBase.FirstOrDefault"/>
        /// </summary>
        public override Recipe FirstOrDefault(Func<Recipe, bool> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="RecipesBase.GetAllAsync"/>
        /// </summary>
        public override async Task<ICollection<Recipe>> GetAllAsync()
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType).Include(i => i.Instructions).ToListAsync();
        }
        /// <summary>
        /// <see cref="RecipesBase.GetByNameAsync"/>
        /// </summary>
        public async override Task<ICollection<Recipe>> GetByNameAsync(string Name)
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType)
                .Include(i => i.Instructions)
                .Where(w => w.Name.Contains(Name))
                .ToListAsync();
        }
        /// <summary>
        /// <see cref="RecipesBase.GetAsync"/>
        /// </summary>
        public override async Task<Recipe> GetAsync(Guid key)
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType).Include(i => i.Instructions).FirstOrDefaultAsync(f => f.Id == key);
        }
        /// <summary>
        /// <see cref="RecipesBase.RemoveAsync"/>
        /// </summary>
        public override async Task RemoveAsync(Recipe entitie)
        {
            Context.Recipes.Remove(entitie);
            await this.SaveAsync();
        }
        /// <summary>
        /// <see cref="RecipesBase.RemoveRangeAsync"/>
        /// </summary>
        public override Task RemoveRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="RecipesBase.SaveAsync"/>
        /// </summary>
        public override async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
        /// <summary>
        /// <see cref="RecipesBase.UpdateAsync"/>
        /// </summary>
        public async override Task UpdateAsync(Recipe entity)
        {
            if (entity is not null)
            {
                Context.Recipes.Update(entity);
                await this.SaveAsync();
            }
        }
        /// <summary>
        /// <see cref="RecipesBase.UpdateRangeAsync"/>
        /// </summary>
        public override Task UpdateRangeAsync(ICollection<Recipe> entities)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="RecipesBase.GetByRecipeByUserIdAsync"/>
        /// </summary>
        public async override Task<ICollection<Recipe>> GetByRecipeByUserIdAsync(Guid userId)
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType)
                    .Include(i => i.Instructions)
                    .Where(w => w.UserId == userId)
                    .ToListAsync();
        }
    }
}
