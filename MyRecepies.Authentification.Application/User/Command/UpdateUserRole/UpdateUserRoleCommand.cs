using MediatR;

namespace MyRecipes.Authentification.Application.User.Command.UpdateUserRole
{
    public class UpdateUserRoleCommand : IRequest
    {
        public Guid UserID { get; set; }
        public string UserRole { get; set; }
        public bool ToAdd { get; set; }
        public UpdateUserRoleCommand(Guid userId, string userRole, bool toAdd)
        {
            UserID = userId;
            UserRole = userRole;
            ToAdd = toAdd;
        }
    }
}
