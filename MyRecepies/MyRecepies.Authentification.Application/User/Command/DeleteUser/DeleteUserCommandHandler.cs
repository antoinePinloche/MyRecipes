using MediatR;
using MyRecipes.Authentification.Domain.Exception;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Authentification.Application.User.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private IUsersRepository _usersRepository;

        public DeleteUserCommandHandler(IUsersRepository usersRepository) => _usersRepository = usersRepository;

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.User userfound =await _usersRepository.GetAsync(request.Guid);
            
            if (userfound is null)
            {
                throw new UserNotFoundException($"User With Guid {request.Guid} doesn't exist");
            }
            await _usersRepository.RemoveAsync(userfound);
        }
    }
}
