using System;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    public class GetIngredientsByFoodTypeIdQueryResult
    {
        public List<Ingredient> Ingredients { get; set; }

        public GetIngredientsByFoodTypeIdQueryResult(List<Ingredient> ingredients) => Ingredients = ingredients;

        public class Ingredient
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string FoodTypeName { get; set; }

            public Ingredient(Guid id, string name, string foodTypeName)
            {
                Id = id;
                Name = name;
                FoodTypeName = foodTypeName;
            }
        }
    }
}
