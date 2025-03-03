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
            var ingredient = await Context.Ingredient.FirstOrDefaultAsync(f => f.Name == entity.Name);
            if (ingredient is not null)
            {
                throw new InvalidOperationException();
            }
            var ingredientAdd = await Context.Ingredient.AddAsync(entity);
            await Context.SaveChangesAsync();
            return ingredientAdd.Entity;
        }
        public override Ingredient FirstOrDefault(Func<Ingredient, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override async Task<ICollection<Ingredient>> GetAllAsync()
        {
            return await Context.Ingredient.Include(i => i.FoodType).ToListAsync();
        }

        public override async Task<Ingredient> GetAsync(Guid key)
        {
            return await Context.Ingredient.Include(i => i.FoodType).FirstOrDefaultAsync(i => i.Id == key);
        }

        public override async Task RemoveAsync(Ingredient entitie)
        {
            Context.Ingredient.Remove(entitie);
            await Context.SaveChangesAsync();
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

        public async override Task<Ingredient> HasIngredient(string Name)
        {
            Ingredient? entityfound = await Context.Ingredient.Include(i => i.FoodType).FirstOrDefaultAsync(f => f.Name == Name);
            return entityfound;
        }

        public override async Task<List<Ingredient>> GetAllIngredientsByFoodTypeId(Guid foodTypeId)
        {
            List<Ingredient>? ingredients = await Context.Ingredient.Where(i => i.FoodTypeId == foodTypeId).Include(e => e.FoodType).ToListAsync();
            return ingredients;
        }

        public override Task<ICollection<Ingredient>> AddRangeAsync(ICollection<Ingredient> entities)
        {
            throw new NotImplementedException();
        }
    }
}
