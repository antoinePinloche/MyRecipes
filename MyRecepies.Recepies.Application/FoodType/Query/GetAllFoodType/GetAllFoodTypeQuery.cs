using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType
{
    public class GetAllFoodTypeQuery : IRequest<GetAllFoodTypeQueryResult>
    {
        public GetAllFoodTypeQuery() { }
    }
}
