//using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.AspNetCore.Identity;

namespace MyRecipes.Authentification.Domain.Entities
{
    public class User : IdentityUser
    {
        public User() : base() { }
    }
}
