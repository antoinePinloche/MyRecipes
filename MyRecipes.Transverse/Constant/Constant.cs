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
            public static readonly string USER = "User";
            public static readonly string ADMIN = "Administrator";
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
