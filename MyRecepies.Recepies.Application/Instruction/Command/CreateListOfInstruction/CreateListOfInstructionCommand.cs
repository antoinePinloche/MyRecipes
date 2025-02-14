using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction
{
    public class CreateListOfInstructionCommand : IRequest
    {
        public List<Instruction> Instructions { get; set; }

        public class Instruction
        {
            public Guid? RecipeId { get; set; }
            public int Step { get; set; }
            public string StepName { get; set; } = string.Empty;
            public string StepInstruction { get; set; } = string.Empty;
            public Instruction(Guid? recipeId, int step, string stepName, string stepInstruction)
            {
                RecipeId = recipeId;
                Step = step;
                StepName = stepName;
                StepInstruction = stepInstruction;
            }
        }
        public CreateListOfInstructionCommand() { }
        public CreateListOfInstructionCommand(List<Instruction> instructions) => Instructions = instructions;
    }
}
