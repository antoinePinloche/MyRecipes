namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientByName
{
    /// <summary>
    /// reponse de la query <see cref="GetIngredientByNameQuery"/>
    /// </summary>
    public class GetIngredientByNameQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FoodTypeName { get; set; }
    }
}
