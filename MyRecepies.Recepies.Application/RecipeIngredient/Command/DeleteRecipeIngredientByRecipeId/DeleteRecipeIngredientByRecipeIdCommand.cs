using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Command.DeleteRecipeIngredientByRecipeId
{
    /// <summary>
    /// Command pour supprimer les RecipeIngredient d'une recette
    /// <see cref="DeleteRecipeIngredientByRecipeIdCommandHandler"/>
    /// </summary>
    public class DeleteRecipeIngredientByRecipeIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteRecipeIngredientByRecipeIdCommand(Guid id) => Id = id;
    }
}
