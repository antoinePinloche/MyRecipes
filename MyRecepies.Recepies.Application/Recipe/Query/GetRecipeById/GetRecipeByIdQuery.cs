using MediatR;

namespace MyRecipes.Recipes.Application.Recipe.Query.GetRecipeById
{
    public class GetRecipeByIdQuery : IRequest<GetRecipeByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetRecipeByIdQuery(Guid id) => Id = id;
    }
}
