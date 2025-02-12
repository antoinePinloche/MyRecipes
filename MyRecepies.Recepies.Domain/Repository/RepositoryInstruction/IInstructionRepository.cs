using MyRecipes.Recipes.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Domain.Repository.RepositoryInstruction
{
    public interface IInstructionRepository : IRepository<Instruction, Guid>
    {
        public Task<ICollection<Instruction>> GetAllInstructionByRecipeIdAsync(Guid Key);
    }
}
