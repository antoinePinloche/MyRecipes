using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryInstruction
{
    /// <summary>
    /// Interface pour le DBContext Instruction
    /// <see cref="IRepository{TEntity, TKey}"/>
    /// </summary>
    public interface IInstructionRepository : IRepository<Instruction, Guid>
    {
        /// <summary>
        /// retourne toutes les instructions d'un recette par son ID
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public Task<ICollection<Instruction>> GetAllInstructionByRecipeIdAsync(Guid Key);
    }
}
