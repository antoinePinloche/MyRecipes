using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.DeleteInstruction
{
    public class DeleteInstructionCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteInstructionCommand(Guid id) => Id = id;
    }
}
