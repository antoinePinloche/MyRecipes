using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById
{
    /// <summary>
    /// Query pour retourner la recette par son ID
    /// <see cref="GetRecipeByIdQueryHandler"/>
    /// </summary>
    public class GetRecipeByIdQuery : IRequest<GetRecipeByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetRecipeByIdQuery(Guid id) => Id = id;
    }
}
