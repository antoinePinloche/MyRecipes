﻿namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName
{
    public class GetIngredientByNameQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FoodTypeName { get; set; }
    }
}
