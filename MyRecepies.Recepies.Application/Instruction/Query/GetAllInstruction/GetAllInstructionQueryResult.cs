﻿namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction
{
    /// <summary>
    /// reponse de la query <see cref="GetAllInstructionQuery"/>
    /// </summary>
    public class GetAllInstructionQueryResult
    {
        public Guid Id { get; set; }
        public int Step { get; set; }
        public string StepName { get; set; } = string.Empty;
        public string StepInstruction { get; set; } = string.Empty;

        public GetAllInstructionQueryResult(Guid id, int step, string stepName, string stepInstruction)
        {
            Id = id;
            Step = step;
            StepName = stepName;
            StepInstruction = stepInstruction;
        }
    }
}
