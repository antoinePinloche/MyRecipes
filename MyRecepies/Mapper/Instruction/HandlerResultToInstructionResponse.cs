using MyRecipes.Web.API.Models.Class.Instruction;
using InstructionQuery = MyRecipes.Recipes.Application.Instruction.Query;

namespace MyRecipes.Web.API.Mapper.Instruction
{
    public static class HandlerResultToInstructionResponse
    {
        public static List<InstructionResponse> ToInstructionResponse(this List<InstructionQuery.GetAllInstructionByRecipeId.GetAllInstructionByRecipeIdQueryResult> ingredientRecipes)
        {
            return ingredientRecipes.Select(i =>
                new InstructionResponse(
                        i.Id,
                        i.Step,
                        i.StepName,
                        i.StepInstruction
                    )
            ).ToList();
        }
    }
}
