using MyRecipes.Recipes.Domain.Entity.Enum;
using MyRecipes.Web.API.Models.Class.Ingredient;

namespace MyRecipes.Web.API.Models.Class.Recipe.Response
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

        public class RecipeIngredientResponse
        {
            public string DisplayName { get; set; } = string.Empty;
            public string FoodTypeDisplayName { get; set; } = string.Empty;
            public double Quantity { get; set; }
            public UnitOfMeasure Unit { get; set; }
        }

        public class InstructionResponse
        {
            public int Step { get; set; }
            public string StepDisplayName { get; set; } = string.Empty;
            public string StepInstruction { get; set; } = string.Empty;
            public InstructionResponse(int step, string stepDisplayName, string stepInstruction)
            {
                Step = step;
                StepDisplayName = stepDisplayName;
                StepInstruction = stepInstruction;
            }
        }

    }
}
