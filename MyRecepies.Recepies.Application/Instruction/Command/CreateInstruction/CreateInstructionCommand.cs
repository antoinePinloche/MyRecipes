using MediatR;

namespace MyRecipes.Recipes.Application.Instruction.Command.CreateInstruction
{
    /// <summary>
    /// Command pour crée L'instruction d'une recette
    /// <see cref="CreateInstructionCommandHandler"/>
    /// </summary>
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
