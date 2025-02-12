﻿using Microsoft.EntityFrameworkCore;
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

        public override async Task<ICollection<Recipe>> GetAllAsync()
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType).Include(i => i.Instructions).ToListAsync();
        }

        public override async Task<Recipe> GetAsync(Guid key)
        {
            return await Context.Recipes.Include(i => i.Ingredients).ThenInclude(th => th.Ingredient.FoodType).Include(i => i.Instructions).FirstOrDefaultAsync(f => f.Id == key);
        }

        public override Task RemoveAsync(Recipe entitie)
        {
            throw new NotImplementedException();
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
    }
}
