using MediatR;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Authentification.Application.User.Command.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
    {
        private IUsersRepository _usersRepository;

        public UpdatePasswordCommandHandler(IUsersRepository usersRepository) => _usersRepository = usersRepository;

        public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.User userFound = await _usersRepository.GetAsync(request.UserId);
            if (userFound != null)
            {
                //userFound.Password = request.NewPassword;
                await _usersRepository.UpdateAsync(userFound);
            }
        }
    }
}
