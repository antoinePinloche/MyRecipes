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
        /// <summary>
        /// <see cref="InstructionBase.AddAsync"/>
        /// </summary>
        public override async Task<Instruction> AddAsync(Instruction entity)
        {
            var entityAdd = await Context.Instructions.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entityAdd.Entity;
        }
        /// <summary>
        /// <see cref="InstructionBase.AddRangeAsync"/>
        /// </summary>
        public async override Task<ICollection<Instruction>> AddRangeAsync(ICollection<Instruction> entities)
        {

            foreach (var entity in entities)
            {
                await this.AddAsync(entity);
            }
            return entities;
        }
        /// <summary>
        /// <see cref="InstructionBase.CreateOrUpdateSchemaAsync"/>
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
        /// <see cref="InstructionBase.FirstOrDefault"/>
        /// </summary>
        public override Instruction FirstOrDefault(Func<Instruction, bool> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// <see cref="InstructionBase.GetAllAsync"/>
        /// </summary>
        public async override Task<ICollection<Instruction>> GetAllAsync()
        {
            return await Context.Instructions.ToListAsync();
        }
        /// <summary>
        /// <see cref="InstructionBase.GetAsync"/>
        /// </summary>
        public override Task<Instruction> GetAsync(Guid key)
        {
            var entity = Context.Instructions.FirstOrDefaultAsync(i => i.Id == key);
            return entity;
        }
        /// <summary>
        /// <see cref="InstructionBase.RemoveAsync"/>
        /// </summary>
        public async override Task RemoveAsync(Instruction entitie)
        {
            Context.Instructions.Remove(entitie);
            await this.SaveAsync();
        }
        /// <summary>
        /// <see cref="InstructionBase.RemoveRangeAsync"/>
        /// </summary>
        public async override Task RemoveRangeAsync(ICollection<Instruction> entities)
        {
            foreach(var entity in entities)
            {
                await RemoveAsync(entity);
            }
        }
        /// <summary>
        /// <see cref="InstructionBase.SaveAsync"/>
        /// </summary>
        public override async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
        /// <summary>
        /// <see cref="InstructionBase.UpdateAsync"/>
        /// </summary>
        public override async Task UpdateAsync(Instruction entity)
        {
            var entityCheck = await Context.Instructions.FirstOrDefaultAsync(f => f.Id == entity.Id);
            if (entityCheck is null)
            {
                throw new Exception();
            }
            Context.Update(entity);
            await this.SaveAsync();
        }
        /// <summary>
        /// <see cref="InstructionBase.UpdateRangeAsync"/>
        /// </summary>
        public async override Task UpdateRangeAsync(ICollection<Instruction> entities)
        {
            foreach (var entity in entities)
            {
                await this.UpdateAsync(entity);
            }
        }
        /// <summary>
        /// <see cref="InstructionBase.GetAllInstructionByRecipeIdAsync"/>
        /// </summary>
        public async override Task<ICollection<Instruction>> GetAllInstructionByRecipeIdAsync(Guid Key)
        {
            return await Context.Instructions.Where(w => w.RecipeId == Key).ToListAsync();
        }
    }
}
