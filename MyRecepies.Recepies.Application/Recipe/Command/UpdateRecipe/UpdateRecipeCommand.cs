using MediatR;
using MyRecipes.Recipes.Domain.Entity.Enum;

namespace MyRecipes.Recipes.Application.Recipe.Command.UpdateRecipe
{
    /// <summary>
    /// Command pour modifier une recette
    /// <see cref="UpdateRecipeCommandHandler"/>
    /// </summary>
    public class UpdateRecipeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Difficulty RecipyDifficulty { get; set; }
        public int TimeToPrepareRecipe { get; set; }
        public int NbGuest { get; set; }

        public UpdateRecipeCommand(Guid id, string name, Difficulty recipyDifficulty, int timeToPrepareRecipe, int nbGuest)
        {
            Id = id;
            Name = name;
            RecipyDifficulty = recipyDifficulty;
            TimeToPrepareRecipe = timeToPrepareRecipe;
            NbGuest = nbGuest;
        }
    }
}
