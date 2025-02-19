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
        public static class CONTROLLER_ROUTE
        {
            public const string INGREDIENT = "api/Ingredient";
            public const string ADMIN_USER = "api/AdminUser";
            public const string FOOD_TYPE = "api/FoodType";
            public const string RECIPE = "api/Recipe";
            public const string RECIPE_INGREDIENT = "api/RecipeIngredient";
            public const string RECIPE_INSTRUCTION = "api/RecipeInstruction";
            public const string AUTHENTIFICATION = "api/Authentification";
        }
        #endregion

        #region Exception
        public static class EXCEPTION
        {
            public static class TITLE
            {
                public static readonly string INVALIDE_KEY = "Invalide key";
                public static readonly string CONFLICT = "Conflict";
                public static readonly string NOT_FOUND = "Not found";
            }
        }
        #endregion
    }
}
