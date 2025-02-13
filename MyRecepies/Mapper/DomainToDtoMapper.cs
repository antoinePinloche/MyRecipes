using MyRecipes.Recipes.Domain.Entity;
using MyRecipes.Web.API.Models.Class.Recipe.Response;
using System.Runtime.CompilerServices;

namespace MyRecipes.Web.API.Mapper
{
    public static class DomainToDtoMapper
    {
        //public static IngredientResponse ToIngredientResponse(this Ingredient recipe)
        //{
        //    return new IngredientResponse
        //    {
        //        DisplayName = recipe.Name,
        //        FoodTypeDisplayName = recipe.FoodType.Name
        //    };
        //}

        public static List<RecipeResponse.InstructionResponse> ToRecipeResponseInstruction(this IEnumerable<Recipes.Domain.Entity.Instruction?> instructions)
        {
            if (instructions is null)
            {
                return new List<RecipeResponse.InstructionResponse>();
            }
            return instructions.Select( i =>
                new RecipeResponse.InstructionResponse(
                    i.Step,
                    i.StepName,
                    i.StepInstruction
                    )
            ).ToList();
        }
        public static List<RecipeResponse.RecipeIngredientResponse> ToRecipeResponseIngredient(this IEnumerable<Recipes.Domain.Entity.RecipeIngredient?> ingredients)
        {
            if (ingredients is null)
            {
                return new List<RecipeResponse.RecipeIngredientResponse>();
            }
            return ingredients.Select( i =>
                new RecipeResponse.RecipeIngredientResponse()
                {
                    
                    Quantity = i.Quantity,
                    Unit = i.Unit,
                    DisplayName = i.Ingredient.Name,
                    FoodTypeDisplayName = i.Ingredient.FoodType.Name
                }
            ).ToList();
        }

        public static List<DetailRecipeResponse.Instruction> ToDetailRecipeResponseInstruction(this IEnumerable<Recipes.Domain.Entity.Instruction> instructions)
        {
            return instructions.Select(i =>
                new DetailRecipeResponse.Instruction()
                {
                    Id = i.Id,
                    Step = i.Step,
                    StepInstruction = i.StepInstruction,
                    StepName = i.StepName,
                }
            ).ToList();
        }

        public static List<DetailRecipeResponse.RecipeIngredient> ToDetailRecipeResponseRecipeIngredient(this IEnumerable<Recipes.Domain.Entity.RecipeIngredient> recipeIngredient)
        {
            return recipeIngredient.Select(i =>
                new DetailRecipeResponse.RecipeIngredient()
                {
                    Id= i.Id,
                    Ingredients = i.Ingredient?.ToDetailRecipeResponseIngredient(),
                    Quantity = i.Quantity,
                    Unit = i.Unit
                })
                .ToList();
        }

        public static DetailRecipeResponse.RecipeIngredient.Ingredient ToDetailRecipeResponseIngredient(this Recipes.Domain.Entity.Ingredient ingredient)
        {
            return new DetailRecipeResponse.RecipeIngredient.Ingredient()
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                FoodTypeIngredient = ingredient.FoodType.ToDetailRecipeResponseFoodType()
            };
        }

        public static DetailRecipeResponse.RecipeIngredient.Ingredient.FoodType ToDetailRecipeResponseFoodType(this Recipes.Domain.Entity.FoodType foodType)
        {
            return new DetailRecipeResponse.RecipeIngredient.Ingredient.FoodType()
            {
                Id = foodType.Id,
                Name = foodType.Name,
            };
        }
    }
}
