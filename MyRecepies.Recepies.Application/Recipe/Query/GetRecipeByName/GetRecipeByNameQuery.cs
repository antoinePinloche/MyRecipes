using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName
{
    /// <summary>
    /// Query pour retourner les recette avec un nom contenant la string
    /// <see cref="GetRecipeByIdQueryHandler"/>
    /// </summary>
    public class GetRecipeByNameQuery : IRequest<List<GetRecipeByNameQueryResult>>
    {
        public string Name { get; set; }
        public GetRecipeByNameQuery(string name) => Name = name;
    }
}
