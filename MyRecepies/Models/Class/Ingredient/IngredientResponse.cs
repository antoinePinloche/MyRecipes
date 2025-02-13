namespace MyRecipes.Web.API.Models.Class.Ingredient
{
    public class IngredientResponse
    {
        public string DisplayName { get; set; } = string.Empty;
        public string FoodTypeDisplayName { get; set; } = string.Empty;

        public IngredientResponse() { }
        public IngredientResponse(string displayName, string foodTypeDisplayName)
        {
            DisplayName = displayName;
            FoodTypeDisplayName = foodTypeDisplayName;
        }
}
