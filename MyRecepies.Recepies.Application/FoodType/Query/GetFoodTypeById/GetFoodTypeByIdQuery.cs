using MediatR;

namespace MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById
{
    /// <summary>
    /// Query pour retourner le FoodType Correspondant a Id
    /// <see cref="GetFoodTypeByIdQueryHandler"/>
    /// </summary>
    public class GetFoodTypeByIdQuery : IRequest<GetFoodTypeByIdQueryResult>
    {
        public Guid Id { get; set; }

        public GetFoodTypeByIdQuery(Guid id) { this.Id = id; }
    }
}
