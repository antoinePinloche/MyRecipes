using Microsoft.AspNetCore.Identity;
using MyRecepies.web.Models.Enum;

namespace MyRecepies.web.Models.Class
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
