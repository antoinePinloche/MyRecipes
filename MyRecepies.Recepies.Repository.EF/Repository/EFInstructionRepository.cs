using Microsoft.EntityFrameworkCore;
using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Repository.EF.DbContext;

namespace MyRecipes.Recipes.Repository.EF.Repository
{
    public class EFInstructionRepository : InstructionBase
    {
        public RecipeDbContext Context { get; set; }

        public EFInstructionRepository(RecipeDbContext context) => Context = context;
        public override async Task<Instruction> AddAsync(Instruction entity)
        {
            var entityAdd = await Context.Instructions.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entityAdd.Entity;
        }

        public override Task<Instruction> AddRangeAsync(ICollection<Instruction> entities)
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

        public override Instruction FirstOrDefault(Func<Instruction, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override Task<ICollection<Instruction>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public override Task<Instruction> GetAsync(Guid key)
        {
            var entity = Context.Instructions.FirstOrDefaultAsync(i => i.Id == key);
            return entity;
        }

        public override Task RemoveAsync(Instruction entitie)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeAsync(ICollection<Instruction> entities)
        {
            throw new NotImplementedException();
        }

        public override Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public override Task UpdateAsync(Instruction entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateRangeAsync(ICollection<Instruction> entities)
        {
            throw new NotImplementedException();
        }
    }
}
