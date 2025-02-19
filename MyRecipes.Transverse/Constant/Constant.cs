using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Transverse.Constant
{
    sealed public class Constant
    {
        #region ROLES
        public static class ROLE
        {
            public const string USER = "User";
            public const string ADMIN = "Administrator";
            public const string ADMINANDUSER = USER + "," + ADMIN;
        }

        #endregion  
        #region CONTROLLER
        public static class CONTROLLER
        {
            public static readonly string INGREDIENT = "Ingredient";
            public static readonly string ADMIN_USER = "AdminUser";
            public static readonly string FOOD_TYPE = "FoodType";
            public static readonly string RECIPE = "Recipe";
            public static readonly string RECIPE_INGREDIENT = "RecipeIngredient";
            public static readonly string RECIPE_INSTRUCTION = "RecipeInstruction";
        }
        #endregion
    }
}
