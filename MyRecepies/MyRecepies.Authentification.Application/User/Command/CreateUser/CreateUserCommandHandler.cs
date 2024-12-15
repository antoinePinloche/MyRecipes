using MediatR;
using MyRecepies.Authentification.Domain.Repository.RepositoryUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Application.User.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUserCommandHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            Domain.Entities.User user = new Domain.Entities.User() { Id = Guid.NewGuid(), Email = request.Email, Password = request.Password, UserName = request.UserName, FirstName = request.FirstName, LastName = request.LastName};
            await _usersRepository.AddAsync(user);
        }
    }
}
