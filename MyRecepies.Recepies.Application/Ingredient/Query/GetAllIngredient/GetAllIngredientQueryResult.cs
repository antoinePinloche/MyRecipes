namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    /// <summary>
    /// reponse de la query <see cref="GetAllIngredientQuery"/>
    /// </summary>
    public class GetAllIngredientQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public FoodType FoodTypeInformation { get; set; }

        public class FoodType
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
