using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientByRecipeId
{
    public class GetRecipeIngredientByRecipeIdQuery : IRequest<List<GetRecipeIngredientByRecipeIdQueryResult>>
    {
        public Guid Id { get; set; }
        public GetRecipeIngredientByRecipeIdQuery(Guid id) => Id = id;
    }
}
