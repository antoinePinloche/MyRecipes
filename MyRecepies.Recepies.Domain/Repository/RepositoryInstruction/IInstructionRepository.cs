using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryInstruction
{
    public interface IInstructionRepository : IRepository<Instruction, Guid>
    {
        public Task<ICollection<Instruction>> GetAllInstructionByRecipeIdAsync(Guid Key);
    }
}
