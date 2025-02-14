namespace MyRecipes.Web.API.Models.Class.Ingredient
{
    public class DetailIngredientResponse
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public FoodType FoodTypeInformation { get; set; }

        public class FoodType
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}
