using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById
{
    /// <summary>
    /// Query pour retourner un RecipeIngredient par son ID
    /// <see cref="GetRecipeIngredientByIdQueryHandler"/>
    /// </summary>
    public class GetRecipeIngredientByIdQuery : IRequest<GetRecipeIngredientByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetRecipeIngredientByIdQuery(Guid id) => Id = id;
    }
}
