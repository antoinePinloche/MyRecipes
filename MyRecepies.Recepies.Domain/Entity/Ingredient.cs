namespace MyRecipes.Recipes.Domain.Entity
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid FoodTypeId { get; set; }
        public FoodType FoodType { get; set; }

    }
}
