using MediatR;
using MyRecepies.Authentification.Domain.Entities;
using MyRecepies.Authentification.Repository.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Application.User.Command.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CreateUserCommand(string username, string email, string password, string firstName, string lastName)
        {
            UserName = username;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
