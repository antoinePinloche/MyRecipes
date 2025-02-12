namespace MyRecipes.Web.API.Models.Class.Ingredient
{
    public class GetAllIngredientResponse
    {
        public List<Ingredient> Ingredients { get; set; }

        public GetAllIngredientResponse() 
        { 
            Ingredients = new List<Ingredient>();
        }
        public class Ingredient
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string FoodType {  get; set; }
            public Ingredient(Guid id, string name, string foodType)
            {
                Id = id;
                Name = name;
                FoodType = foodType;
            }
        }
    }
}
