namespace MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById
{
    /// <summary>
    /// reponse de la query <see cref="GetInstructionByIdQuery"/>
    /// </summary>
    public class GetInstructionByIdQueryResult
    {
        public Guid Id { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;

        public GetInstructionByIdQueryResult(Guid id, int step, string stepName, string stepInstruction)
        {
            Id = id;
            Step = step;
            StepName = stepName;
            StepInstruction = stepInstruction;
        }
    }
}
