using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeByName
{
    public class GetRecipeByNameQuery : IRequest<List<GetRecipeByNameQueryResult>>
    {
        public string Name { get; set; }
        public GetRecipeByNameQuery(string name) => Name = name;
    }
}
