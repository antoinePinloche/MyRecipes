using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Domain.Entity
{
    public class Recipe
    {
        public Guid id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; }
        public IEnumerable<Instruction> Instructions { get; set; }
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }
    }
}
