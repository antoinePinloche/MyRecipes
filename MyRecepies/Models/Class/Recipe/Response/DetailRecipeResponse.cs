using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Web.API.Models.Class.Recipe.Response
{
    public class DetailRecipeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RecipeIngredient>? Ingredients { get; set; }
        public List<Instruction>? Instructions { get; set; }
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }

        public class RecipeIngredient
        {
            public Guid Id { get; set; }
            public Ingredient? Ingredients { get; set; }
            public double Quantity { get; set; }
            public UnitOfMeasure Unit { get; set; }

            public class Ingredient
            {
                public Guid Id { get; set; }
                public string Name { get; set; } = string.Empty;
                public FoodType FoodTypeIngredient { get; set; }

                public class FoodType
                {
                    public Guid Id { get; set; }
                    public string Name { get; set; }
                }
            }
        }

        public class Instruction
        {
            public Guid Id { get; set; }
            public int Step { get; set; }
            public string StepName { get; set; } = string.Empty;
            public string StepInstruction { get; set; } = string.Empty;
        }
    }
}
