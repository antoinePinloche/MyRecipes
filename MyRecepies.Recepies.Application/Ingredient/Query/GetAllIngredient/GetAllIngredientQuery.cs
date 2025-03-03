using MediatR;

namespace MyRecipes.Recipes.Application.Ingredient.Query.GetAllIngredient
{
    /// <summary>
    /// Query pour retourner tous les Ingredien
    /// <see cref="GetAllIngredientQueryHandler"/>
    /// </summary>
    public class GetAllIngredientQuery : IRequest<List<GetAllIngredientQueryResult>>
    {
        public GetAllIngredientQuery() { }
    }
}
