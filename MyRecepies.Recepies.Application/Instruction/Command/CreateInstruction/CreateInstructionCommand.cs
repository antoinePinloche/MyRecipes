using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction
{
    public class CreateInstructionCommand : IRequest
    {
        public Guid? RecipeId { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;

        public CreateInstructionCommand(Guid? recipeId, int step, string stepName, string stepInstruction)
        {
            RecipeId = recipeId;
            Step = step;
            StepName = stepName;
            StepInstruction = stepInstruction;
        }
    }
}
