using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Authentification.Application.User.Command.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest
    {
        public string NewPassword { get; set; }
        public Guid UserId { get; set; }

        public UpdatePasswordCommand(string newPassword, Guid userId)
        {
            NewPassword = newPassword;
            UserId = userId;
        }
    }
}
