using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Domain.Entity
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IngredientCategory IngredientCategory { get; set; }
    }
}
