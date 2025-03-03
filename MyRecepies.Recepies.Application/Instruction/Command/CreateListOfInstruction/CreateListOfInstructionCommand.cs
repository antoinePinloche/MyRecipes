using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateListOfInstruction
{
    /// <summary>
    /// Command pour crée une liste d'instruction d'une recette
    /// <see cref="CreateListOfInstructionCommandHandler"/>
    /// </summary>
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
