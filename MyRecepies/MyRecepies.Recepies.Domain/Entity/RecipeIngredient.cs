using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Domain.Entity
{
    public class RecipeIngredient
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public double Quantity { get; set; }
        public UnitOfMeasure Unit {  get; set; }
    }
}
