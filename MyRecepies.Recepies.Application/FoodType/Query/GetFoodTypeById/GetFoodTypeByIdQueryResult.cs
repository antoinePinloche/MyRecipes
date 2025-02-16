namespace MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById
{
    public class GetFoodTypeByIdQueryResult
    {
        public Domain.Entity.FoodType FoodType { get; set; }

        public GetFoodTypeByIdQueryResult(Domain.Entity.FoodType ft) {
            FoodType = ft;
        }
    }
}
