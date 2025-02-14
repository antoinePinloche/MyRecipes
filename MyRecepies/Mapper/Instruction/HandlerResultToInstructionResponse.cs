using MyRecipes.Web.API.Models.Class.Instruction.Response;
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

        public static InstructionResponse ToInstructionResponse(this InstructionQuery.GetInstructionById.GetInstructionByIdQueryResult ingredientRecipes)
        {
            return new InstructionResponse(
                        ingredientRecipes.Id,
                        ingredientRecipes.Step,
                        ingredientRecipes.StepName,
                        ingredientRecipes.StepInstruction
                    );
        }

        public static List<InstructionResponse> ToInstructionResponse(this List<InstructionQuery.GetAllInstruction.GetAllInstructionQueryResult> ingredientRecipes)
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
