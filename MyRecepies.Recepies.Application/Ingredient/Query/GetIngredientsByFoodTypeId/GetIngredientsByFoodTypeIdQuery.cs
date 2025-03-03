using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetIngredientsByFoodTypeId
{
    /// <summary>
    /// Query pour retourner les Ingredien correspondant au foodTypeId
    /// <see cref="GetIngredientByNameQueryHandler"/>
    /// </summary>
    public class GetIngredientsByFoodTypeIdQuery : IRequest<List<GetIngredientsByFoodTypeIdQueryResult>>
    {
        public Guid Id { get; set; }

        public GetIngredientsByFoodTypeIdQuery(Guid id) => Id = id;
    }
}
