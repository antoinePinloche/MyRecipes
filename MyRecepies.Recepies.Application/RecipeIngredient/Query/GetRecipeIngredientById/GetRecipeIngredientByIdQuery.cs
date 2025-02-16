using MediatR;

namespace MyRecipes.Recipes.Application.RecipeIngredient.Query.GetRecipeIngredientById
{
    public class GetRecipeIngredientByIdQuery : IRequest<GetRecipeIngredientByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetRecipeIngredientByIdQuery(Guid id) => Id = id;
    }
}
