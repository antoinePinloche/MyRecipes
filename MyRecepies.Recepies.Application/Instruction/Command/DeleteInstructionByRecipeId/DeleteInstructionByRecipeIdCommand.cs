using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstructionByRecipeId
{
    public class DeleteInstructionByRecipeIdCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteInstructionByRecipeIdCommand(Guid id) => Id = id;
    }
}
