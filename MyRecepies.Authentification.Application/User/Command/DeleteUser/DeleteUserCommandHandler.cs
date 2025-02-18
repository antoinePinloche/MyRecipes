using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Authentification.Application.User.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private IUsersRepository _usersRepository;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IServiceProvider _serviceProvider;
        public DeleteUserCommandHandler(IUsersRepository usersRepository, IServiceProvider serviceProvider, ILogger<DeleteUserCommandHandler> logger)
        {
            _usersRepository = usersRepository;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Guid.IsEmpty())
                {
                    throw new WrongParameterException("Invalide Key", "User Id is empty");
                }
                Domain.Entities.User userfound = await _usersRepository.GetAsync(request.Guid);

                if (userfound is null)
                {
                    throw new UserNotFoundException("Invalide key ",$"User With Guid {request.Guid} doesn't exist");
                }
                var userManager = _serviceProvider.GetRequiredService<UserManager<Domain.Entities.User>>();
                if (userManager is not null)
                {
                    var roles = await userManager.GetRolesAsync(userfound);
                    foreach (var role in roles)
                    {
                        var result = await userManager.RemoveFromRoleAsync(userfound, role);
                    }
                    await _usersRepository.RemoveAsync(userfound);
                }

                _logger.LogInformation($"DeleteUserCommand User {request.Guid} Delete with success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
