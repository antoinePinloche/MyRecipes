using Microsoft.AspNetCore.Mvc;

namespace MyRecipes.Transverse.Extension
{
    public static class ControllerBaseExtension
    {
        public static bool CheckIsAdmin(this ControllerBase controllerBase)
        {
            var userRole = controllerBase.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            if (!userRole.IsNullOrEmpty() && userRole != Constant.Constant.ROLE.ADMIN)
            {
                return false;
            }
            return true;
        }

        public static Guid GetUserGuid(this ControllerBase controllerBase)
        {
            var user = controllerBase.User?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (!Guid.TryParse(user, out Guid userId))
            {
                throw new System.Exception();
            }
            return userId;
        }
    }
}
