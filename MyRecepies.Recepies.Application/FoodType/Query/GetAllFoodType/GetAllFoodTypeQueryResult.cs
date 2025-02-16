namespace MyRecipes.Recipes.Application.FoodType.Query.GetAllFoodType
{
    public class GetAllFoodTypeQueryResult
    {
        public List<Domain.Entity.FoodType> FoodType {  get; set; }

        public GetAllFoodTypeQueryResult(List<Domain.Entity.FoodType> foodType) => FoodType = foodType;
    }
}
