using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MyRecipes.Transverse.Constant
{
    /// <summary>
    /// Class contenants les constantes du projet
    /// </summary>
    sealed public class Constant
    {
        #region ROLES
        /// <summary>
        /// Class avec le noms des role du projet
        /// </summary>
        public static class ROLE
        {
            public const string USER = "User";
            public const string ADMIN = "Administrator";
            public const string ADMINANDUSER = USER + "," + ADMIN;
        }

        #endregion  
        #region CONTROLLER
        /// <summary>
        /// Route de l'api
        /// </summary>
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
            /// <summary>
            /// Titre retourner pour les exceptions
            /// </summary>
            public static class TITLE
            {
                public static readonly string INVALIDE_KEY = "Invalide key";
                public static readonly string INVALIDE_PARAMETER = "Invalide parameter";
                public static readonly string CONFLICT = "Conflict";
                public static readonly string INSTRUCTION_DUPLICATION_CREATE = "Can't Create instruction step already Exist";
                public static readonly string INSTRUCTION_DUPLICATION_UPDATE = "Can't update instruction with new step because step already Exist";
                public static readonly string NOT_FOUND = "Not found";
                public static readonly string FORBIDDEN = "Forbidden";
            }
            /// <summary>
            /// Message basique pour les lever d'exceptions
            /// </summary>
            public static class WRONG_PARAMETER_MESSAGE
            {
                public static readonly string FORBIDDEN = "Ressource isn't accessible for you with your access authorization";
                public static readonly string ID = "request paramater Id is empty or null";
                public static readonly string MODEL = "request paramater model is empty or null";
                public static readonly string NAME = "request paramater Name is empty or null";
                public static readonly string USER_ROLE = "request paramater UserRole is empty or null";
                public static readonly string DUPLICATION_INSTRUCTION = "Try to add same instruction with same step for the same recipe";
                public static readonly string DUPLICATION_STEP = "Try to add same step for the same recipe";
                public static readonly string STEP_INSTRUCTION = "request paramater StepInstruction is empty or null";
                public static readonly string STEP_NAME = "request paramater StepName is empty or null";
                public static readonly string USER_ID = "request paramater UserId is empty or null";
                public static readonly string RECIPE_ID = "request paramater RecipeId is empty or null";
                public static readonly string RECIPE_INGREDIENT_ID = "request paramater RecipeIngredientId is empty or null";
                public static readonly string INGREDIENT_ID = "request paramater IngredientId is empty or null";
                public static readonly string INSTRUCTION = "request paramater Instruction is empty or null";
                public static readonly string INSTRUCTION_ID = "request paramater InstructionId is empty or null";
                public static readonly string FOOD_TYPE_ID = "request paramater FoodTypeId is empty or null";
                public static readonly string RECIPE_NOT_FOUND = "Recipe not found";
            }
        }
        #endregion

        #region Claims
        public static class CLAIMS
        {
            public const string ROLE = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            public const string NAME_IDENTIFIER = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        }
        #endregion
    }
}
