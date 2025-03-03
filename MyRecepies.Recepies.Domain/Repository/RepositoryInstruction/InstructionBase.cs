using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryInstruction
{
    public abstract class InstructionBase : IInstructionRepository
    {
        public abstract Task<Instruction> AddAsync(Instruction entity);
        public abstract Task<ICollection<Instruction>> AddRangeAsync(ICollection<Instruction> entities);
        public abstract Instruction FirstOrDefault(Func<Instruction, bool> predicate);
        public abstract Task<ICollection<Instruction>> GetAllAsync();
        public abstract Task<Instruction> GetAsync(Guid key);
        public abstract Task RemoveAsync(Instruction entitie);
        public abstract Task RemoveRangeAsync(ICollection<Instruction> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(Instruction entity);
        public abstract Task UpdateRangeAsync(ICollection<Instruction> entities);
        public abstract Task CreateOrUpdateSchemaAsync();
        public abstract Task<ICollection<Instruction>> GetAllInstructionByRecipeIdAsync(Guid Key);
    }
}
