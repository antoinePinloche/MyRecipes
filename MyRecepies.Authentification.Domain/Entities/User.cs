//using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.AspNetCore.Identity;

namespace MyRecipes.Authentification.Domain.Entities
{
    public class User : IdentityUser
    {
        public User() : base() { }
        //public Guid Id { get; set; }
        //public string UserName { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty;
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
    }
}
