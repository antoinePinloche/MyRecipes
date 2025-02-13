using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Web.API.Models.Class.Instruction;
using MyRecipes.Web.API.Models.Class.RecipeIngredient;

namespace MyRecipes.Web.API.Models.Class.Recipe
{
    public class RecipeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<RecipeIngredientResponse>? Ingredients { get; set; }
        public IEnumerable<InstructionResponse>? Instructions { get; set; }
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }
    }
}
