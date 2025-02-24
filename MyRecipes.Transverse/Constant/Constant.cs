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
                public static readonly string INVALIDE_PARAMETER = "Invalide parameter";
                public static readonly string CONFLICT = "Conflict";
                public static readonly string NOT_FOUND = "Not found";
                public static readonly string FORBIDDEN = "Forbidden";
            }
            public static class WRONG_PARAMETER_MESSAGE
            {
                public static readonly string FORBIDDEN = "Ressource isn't accessible for you with your access authorization";
                public static readonly string ID = "request paramater Id is empty or null";
                public static readonly string MODEL = "request paramater model is empty or null";
                public static readonly string NAME = "request paramater Name is empty or null";
                public static readonly string USER_ROLE = "request paramater UserRole is empty or null";
                public static readonly string DUPLICATION_INSTRUCTION = "Try to add same instruction with same step for the same recipe";
                public static readonly string STEP_INSTRUCTION = "request paramater StepInstruction is empty or null";
                public static readonly string STEP_NAME = "request paramater StepName is empty or null";
                public static readonly string USER_ID = "request paramater UserId is empty or null";
                public static readonly string RECIPE_ID = "request paramater RecipeId is empty or null";
                public static readonly string INGREDIENT_ID = "request paramater IngredientId is empty or null";
                public static readonly string INSTRUCTION_ID = "request paramater InstructionId is empty or null";
                public static readonly string FOOD_TYPE_ID = "request paramater FoodTypeId is empty or null";
            }
        }
        #endregion
    }
}
