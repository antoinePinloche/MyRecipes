using Microsoft.EntityFrameworkCore;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Repository.EF.DbContext;
using MyRecipes.Transverse.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Repository.EF.Repository
{
    public class EFFoodTypeRepository : FoodTypeBase
    {
        public RecipeDbContext Context { get; set; }

        public EFFoodTypeRepository(RecipeDbContext context) => Context = context;

        public override async Task<FoodType> AddAsync(FoodType entity)
        {
            var entityCheck = await Context.FoodTypes.FirstOrDefaultAsync(w => w.Name.ToUpper() == entity.Name.ToUpper());
            if (entityCheck is not null)
            {
                throw new FoodTypeAlreadyExistException(nameof(AddAsync), Path.GetFullPath("EFFoodTypeRepository"), "invalide creation", $"FoodType {entity.Name} already exist");
            }
            var entityAdd = await Context.FoodTypes.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public override Task<FoodType> AddRangeAsync(ICollection<FoodType> entities)
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

        public override FoodType FirstOrDefault(Func<FoodType, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public async override Task<ICollection<FoodType>> GetAllAsync()
        {
            var result = await Context.FoodTypes.ToListAsync();
            return result;
        }

        public async override Task<FoodType> GetAsync(Guid key)
        {
            FoodType? foodTypeFound = await Context.FoodTypes.FirstOrDefaultAsync(f => f.Id == key);
            if (foodTypeFound is null)
                return null;
            return (FoodType)foodTypeFound;
        }

        public async override Task RemoveAsync(FoodType entitie)
        {
            FoodType? foodTypeFound = await Context.FoodTypes.FirstOrDefaultAsync(f => f == entitie);
            if (foodTypeFound != null)
            {
                Context.FoodTypes.Remove(entitie);
                await Context.SaveChangesAsync();
            }
        }

        public override Task RemoveRangeAsync(ICollection<FoodType> entities)
        {
            throw new NotImplementedException();
        }

        public async override Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(FoodType entity)
        {
            Context.FoodTypes.Update(entity);
            await Context.SaveChangesAsync();
        }

        public override Task UpdateRangeAsync(ICollection<FoodType> entities)
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> FoodTypeByName(string name)
        {
            FoodType? foodType = await Context.FoodTypes.FirstOrDefaultAsync(f => f.Name == name);
            if (foodType is null) 
                return false;
            return true;
        }
    }
}
