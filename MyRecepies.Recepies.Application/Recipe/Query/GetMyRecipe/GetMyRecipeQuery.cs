using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetMyRecipe
{
    /// <summary>
    /// Query pour retourner toutes les recettes de l'utilisateur
    /// <see cref="GetMyRecipeQueryHandler"/>
    /// </summary>
    public class GetMyRecipeQuery: IRequest<List<GetMyRecipeQueryResult>>
    {
        public Guid Id { get; set; }

        public GetMyRecipeQuery(Guid guid) { Id = guid; }
    }
}
