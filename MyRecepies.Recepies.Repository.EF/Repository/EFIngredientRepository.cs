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
        /// <summary>
        /// <see cref="IngredientBase.AddAsync"/>
        /// </summary>
        public override async Task<Ingredient> AddAsync(Ingredient entity)
        {
            var ingredient = await Context.Ingredient.FirstOrDefaultAsync(f => f.Name == entity.Name);
            if (ingredient is not null)
            {
                throw new InvalidOperationException();
            }
            var ingredientAdd = await Context.Ingredient.AddAsync(entity);
            await Context.SaveChangesAsync();
            return ingredientAdd.Entity;
        }
        /// <summary>
        /// <see cref="IngredientBase.FirstOrDefault"/>
        /// </summary>
        public override Ingredient FirstOrDefault(Func<Ingredient, bool> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="IngredientBase.GetAllAsync"/>
        /// </summary>
        public override async Task<ICollection<Ingredient>> GetAllAsync()
        {
            return await Context.Ingredient.Include(i => i.FoodType).ToListAsync();
        }
        /// <summary>
        /// <see cref="IngredientBase.GetAsync"/>
        /// </summary>
        public override async Task<Ingredient> GetAsync(Guid key)
        {
            return await Context.Ingredient.Include(i => i.FoodType).FirstOrDefaultAsync(i => i.Id == key);
        }
        /// <summary>
        /// <see cref="IngredientBase.RemoveAsync"/>
        /// </summary>
        public override async Task RemoveAsync(Ingredient entitie)
        {
            Context.Ingredient.Remove(entitie);
            await Context.SaveChangesAsync();
        }
        /// <summary>
        /// <see cref="IngredientBase.RemoveRangeAsync"/>
        /// </summary>
        public override Task RemoveRangeAsync(ICollection<Ingredient> entities)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="IngredientBase.SaveAsync"/>
        /// </summary>
        public override Task SaveAsync()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="IngredientBase.UpdateAsync"/>
        /// </summary>
        public override Task UpdateAsync(Ingredient entity)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="IngredientBase.UpdateRangeAsync"/>
        /// </summary>
        public override Task UpdateRangeAsync(ICollection<Ingredient> entities)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="IngredientBase.HasIngredient"/>
        /// </summary>
        public async override Task<Ingredient> HasIngredient(string Name)
        {
            Ingredient? entityfound = await Context.Ingredient.Include(i => i.FoodType).FirstOrDefaultAsync(f => f.Name == Name);
            return entityfound;
        }
        /// <summary>
        /// <see cref="IngredientBase.GetAllIngredientsByFoodTypeId"/>
        /// </summary>
        public override async Task<List<Ingredient>> GetAllIngredientsByFoodTypeId(Guid foodTypeId)
        {
            List<Ingredient>? ingredients = await Context.Ingredient.Where(i => i.FoodTypeId == foodTypeId).Include(e => e.FoodType).ToListAsync();
            return ingredients;
        }
        /// <summary>
        /// <see cref="IngredientBase.AddRangeAsync"/>
        /// </summary>
        public override Task<ICollection<Ingredient>> AddRangeAsync(ICollection<Ingredient> entities)
        {
            throw new NotImplementedException();
        }
    }
}
