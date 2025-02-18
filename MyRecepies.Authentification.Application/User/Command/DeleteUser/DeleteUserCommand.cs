using MediatR;

namespace MyRecipes.Authentification.Application.User.Command.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Guid { get; set; }
        public DeleteUserCommand(Guid guid)
        {
            Guid = guid;
        }
    }
}
