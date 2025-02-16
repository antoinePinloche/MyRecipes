namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId
{
    public class GetAllInstructionByRecipeIdQueryResult
    {
        public Guid Id { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;

        public GetAllInstructionByRecipeIdQueryResult(Guid id, int step, string stepName, string stepInstruction)
        {
            Id = id;
            Step = step;
            StepName = stepName;
            StepInstruction = stepInstruction;
        }
    }
}
