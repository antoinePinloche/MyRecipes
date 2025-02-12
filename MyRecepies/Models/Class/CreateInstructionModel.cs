namespace MyRecipes.Web.API.Models.Class
{
    public class CreateInstructionModel
    {
        public Guid? RecipeId { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;

        public CreateInstructionModel(Guid? recipeId, int step, string stepName, string stepInstruction)
        {
            RecipeId = recipeId;
            Step = step;
            StepName = stepName;
            StepInstruction = stepInstruction;
        }
    }
}
