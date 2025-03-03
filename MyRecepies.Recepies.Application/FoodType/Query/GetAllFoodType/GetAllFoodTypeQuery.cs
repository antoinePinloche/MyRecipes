using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType
{
    /// <summary>
    /// Query pour retourner tous les FoodType
    /// <see cref="GetAllFoodTypeQueryHandler"/>
    /// </summary>
    public class GetAllFoodTypeQuery : IRequest<GetAllFoodTypeQueryResult>
    {
        public GetAllFoodTypeQuery() { }
    }
}
