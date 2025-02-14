using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById
{
    public class GetFoodTypeByIdQuery : IRequest<GetFoodTypeByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetFoodTypeByIdQuery(Guid id) { this.Id = id; }
    }
}
