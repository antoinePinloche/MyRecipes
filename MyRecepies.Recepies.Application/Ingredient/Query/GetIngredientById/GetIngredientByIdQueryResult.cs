namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    /// <summary>
    /// reponse de la query <see cref="GetIngredientByIdQuery"/>
    /// </summary>
    public class GetIngredientByIdQueryResult
    {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string FoodTypeName { get; set; }
    }
}
