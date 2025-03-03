using MyRecipes.Recipes.Domain.Entity;

namespace MyRecipes.Web.API.Models.Class.Instruction.Model
{
    public class UpdateInstructionModel
    {
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;

        public UpdateInstructionModel(int step, string stepName, string stepInstruction)
        {
            Step = step;
            StepName = stepName;
            StepInstruction = stepInstruction;
        }
    }
}
