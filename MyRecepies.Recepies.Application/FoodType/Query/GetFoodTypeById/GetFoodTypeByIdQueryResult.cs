namespace MyRecipes.Recipes.Application.FoodType.Query.GetFoodTypeById
{
    /// <summary>
    /// Resultat de la query <see cref="GetFoodTypeByIdQuery"/>
    /// </summary>
    public class GetFoodTypeByIdQueryResult
    {
        public Domain.Entity.FoodType FoodType { get; set; }

        public GetFoodTypeByIdQueryResult(Domain.Entity.FoodType ft) {
            FoodType = ft;
        }
    }
}
