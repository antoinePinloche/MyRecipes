using System.ComponentModel.DataAnnotations;

namespace MyRecipes.Web.API.Models.Class.FoodType
{
    public class UpdateFoodTypeModel
    {
        [Required]
        public string Name { get; set; }
        public UpdateFoodTypeModel(string name) => Name = name;
    }
}
