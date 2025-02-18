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
        }
        #endregion
    }
}
