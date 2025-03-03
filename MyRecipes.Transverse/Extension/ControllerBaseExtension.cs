using Microsoft.AspNetCore.Mvc;
using MyRecipes.Transverse.Constant;

namespace MyRecipes.Transverse.Extension
{
    /// <summary>
    /// Methode d'extension pour la class ControllerBase
    /// </summary>
    public static class ControllerBaseExtension
    {
        /// <summary>
        /// Methode pour savoir le user est un administrateur
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <returns></returns>
        public static bool CheckIsAdmin(this ControllerBase controllerBase)
        {
            var userRole = controllerBase.User?.FindFirst(Constant.Constant.CLAIMS.ROLE)?.Value;
            if (!userRole.IsNullOrEmpty() && userRole != Constant.Constant.ROLE.ADMIN)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Methode retournant le Guid de l'utilisateur
        /// </summary>
        /// <param name="controllerBase"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public static Guid GetUserGuid(this ControllerBase controllerBase)
        {
            var user = controllerBase.User?.FindFirst(Constant.Constant.CLAIMS.NAME_IDENTIFIER)?.Value;
            if (!Guid.TryParse(user, out Guid userId))
            {
                throw new System.Exception();
            }
            return userId;
        }
    }
}
