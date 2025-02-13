using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Web.API.Models.Class.Recipe.Response
{
    public class RecipeByNameReponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }
    }
}
