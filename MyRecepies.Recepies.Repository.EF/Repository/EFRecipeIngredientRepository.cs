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
        /// <summary>
        /// <see cref="RecipeIngredientBase.AddAsync"/>
        /// </summary>
        public override async Task<RecipeIngredient> AddAsync(RecipeIngredient entity)
        {
            var entityAdd = await Context.RecipeIngredients.AddAsync(entity);
            await this.SaveAsync();
            return entityAdd.Entity;
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.AddRangeAsync"/>
        /// </summary>
        public override Task<ICollection<RecipeIngredient>> AddRangeAsync(ICollection<RecipeIngredient> entities)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.FirstOrDefault"/>
        /// </summary>
        public override RecipeIngredient FirstOrDefault(Func<RecipeIngredient, bool> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.GetAllAsync"/>
        /// </summary>
        public override async Task<ICollection<RecipeIngredient>> GetAllAsync()
        {
            var entitiesFound = await Context.RecipeIngredients.Include(i => i.Ingredient).Include(i => i.Ingredient.FoodType).ToListAsync();
            return entitiesFound;
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.GetAllRecipeIngredientByRecipeIdlAsync"/>
        /// </summary>
        public override async Task<ICollection<RecipeIngredient>> GetAllRecipeIngredientByRecipeIdlAsync(Guid Key)
        {
            var entitiesFound = await Context.RecipeIngredients.Include(i => i.Ingredient)
                .Include(i => i.Ingredient.FoodType)
                .Where(w => w.RecipeId == Key)
                .ToListAsync();
            return entitiesFound;
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.GetAsync"/>
        /// </summary>
        public override async Task<RecipeIngredient> GetAsync(Guid key)
        {
            var entityFound = await Context.RecipeIngredients.Include(i => i.Ingredient).Include(i => i.Ingredient.FoodType).FirstOrDefaultAsync(f => f.Id == key);
            return entityFound;
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.RemoveAsync"/>
        /// </summary>
        public override async Task RemoveAsync(RecipeIngredient entitie)
        {
            if (entitie is not null)
            {
                Context.ChangeTracker.Clear();
                Context.RecipeIngredients.Remove(entitie);
                await this.SaveAsync();
            }
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.RemoveRangeAsync"/>
        /// </summary>
        public override async Task RemoveRangeAsync(ICollection<RecipeIngredient> entities)
        {
            foreach(var entity in entities)
            {
                await this.RemoveAsync(entity);
            }
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.SaveAsync"/>
        /// </summary>
        public override async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.UpdateAsync"/>
        /// </summary>
        public override async Task UpdateAsync(RecipeIngredient entity)
        {
            Context.ChangeTracker.Clear();
            Context.RecipeIngredients.Update(entity);
            await this.SaveAsync();
        }
        /// <summary>
        /// <see cref="RecipeIngredientBase.UpdateRangeAsync"/>
        /// </summary>
        public async override Task UpdateRangeAsync(ICollection<RecipeIngredient> entities)
        {
            foreach(var entity in entities)
            {
                await this.UpdateAsync(entity);
            }
        }
    }
}
