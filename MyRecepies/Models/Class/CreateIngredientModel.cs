using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Web.API.Models.Class
{
    public class CreateIngredientModel
    {
        public string Name { get; set; } = string.Empty;
        public Guid IngredientCategoryId { get; set; }
    }
}
