using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryInstruction
{
    /// <summary>
    /// Class abstraite repressentant la base des appels en DB pour l'entité Instruction
    /// <see cref="IInstructionRepository"/>
    /// </summary>
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
        /// <summary>
        /// <see cref="IInstructionRepository.GetAllInstructionByRecipeIdAsync"/> 
        /// </summary>
        public abstract Task<ICollection<Instruction>> GetAllInstructionByRecipeIdAsync(Guid Key);
    }
}
