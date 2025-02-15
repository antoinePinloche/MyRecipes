namespace MyRecipes.Web.API.Models.Class.Ingredient
{
    public class IngredientResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string FoodTypeDisplayName { get; set; } = string.Empty;

        public IngredientResponse() { }
        public IngredientResponse(Guid id, string displayName, string foodTypeDisplayName)
        {
            Id = id;
            DisplayName = displayName;
            FoodTypeDisplayName = foodTypeDisplayName;
        }
    }
}
