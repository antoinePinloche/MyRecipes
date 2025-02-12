using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Instruction.Query.GetAllInstruction
{
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
