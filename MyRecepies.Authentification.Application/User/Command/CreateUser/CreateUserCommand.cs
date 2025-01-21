using MediatR;

namespace MyRecipes.Authentification.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CreateUserCommand(string username, string email, string password)
        {
            UserName = username;
            Email = email;
            Password = password;
        }
    }
}
