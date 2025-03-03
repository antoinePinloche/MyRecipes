namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    /// <summary>
    /// reponse de la query <see cref="GetIngredientsByFoodTypeIdQuery"/>
    /// </summary>
    public class GetIngredientsByFoodTypeIdQueryResult
    {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string FoodTypeName { get; set; }
    }
}
