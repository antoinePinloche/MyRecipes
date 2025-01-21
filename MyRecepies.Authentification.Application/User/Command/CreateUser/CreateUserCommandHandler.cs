using MediatR;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;

namespace MyRecipes.Authentification.Application.User.Command.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUserCommandHandler(IUsersRepository usersRepository) => _usersRepository = usersRepository;

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            Domain.Entities.User user = new Domain.Entities.User() { UserName = request.UserName, Email = request.Email, PasswordHash = request.Password, NormalizedUserName = request.UserName.ToUpper(), NormalizedEmail = request.Email.ToUpper() };
            await _usersRepository.AddAsync(user);
        }
    }
}
