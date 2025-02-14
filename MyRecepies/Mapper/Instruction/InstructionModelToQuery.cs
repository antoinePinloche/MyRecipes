using GetByID = MyRecipes.Recipes.Application.Instruction.Query.GetInstructionById;
using GetByRecipe = MyRecipes.Recipes.Application.Instruction.Query.GetAllInstructionByRecipeId;

namespace MyRecipes.Web.API.Mapper.Instruction
{
    public static class InstructionModelToQuery
    {
        public static GetByID.GetInstructionByIdQuery ToInstructionByIdQuery(this Guid id)
        {
            return new GetByID.GetInstructionByIdQuery(id);
        }

        public static GetByRecipe.GetAllInstructionByRecipeIdQuery ToAllInstructionByRecipeIdQuery(this Guid id)
        {
            return new GetByRecipe.GetAllInstructionByRecipeIdQuery(id);
        }
    }
}
