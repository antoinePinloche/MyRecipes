using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientById
{
    public class GetIngredientByIdQuery : IRequest<GetIngredientByIdQueryResult>
    {
        public Guid Id { get; set; }
        public GetIngredientByIdQuery(Guid id) => Id = id;
    }
}
