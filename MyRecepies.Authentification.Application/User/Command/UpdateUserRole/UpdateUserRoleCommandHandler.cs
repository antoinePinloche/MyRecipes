using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Authentification.Application.User.Command.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand>
    {
        private IUsersRepository _usersRepository;
        private readonly ILogger<UpdateUserRoleCommandHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public UpdateUserRoleCommandHandler(IUsersRepository usersRepository, IServiceProvider serviceProvider, ILogger<UpdateUserRoleCommandHandler> logger)
        {
            _usersRepository = usersRepository;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserID.IsEmpty())
                {
                    throw new WrongParameterException(Constant.EXCEPTION.TITLE.INVALIDE_KEY, Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.USER_ID);
                }
                if (request.UserRole.IsNullOrEmpty())
                {
                    throw new WrongParameterException(Constant.EXCEPTION.TITLE.INVALIDE_KEY, Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.USER_ROLE);
                }
                Domain.Entities.User userfound = await _usersRepository.GetAsync(request.UserID);

                if (userfound is null)
                {
                    throw new UserNotFoundException(Constant.EXCEPTION.TITLE.INVALIDE_KEY, $"User With Guid {request.UserID} doesn't exist");
                }
                var userManager = _serviceProvider.GetRequiredService<UserManager<Domain.Entities.User>>();
                var roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (userManager is not null && roleManager is not null)
                {
                    var userRoles = await userManager.GetRolesAsync(userfound);
                    
                    foreach (var role in userRoles)
                    {
                        if (role == request.UserRole && request.ToAdd)
                        {
                            throw new UserRoleAlreadyExistException(Constant.EXCEPTION.TITLE.CONFLICT, $"User {userfound.UserName} already Have the role  {request.UserRole}");
                        }
                        else if (role == request.UserRole && !request.ToAdd)
                        {
                            await userManager.RemoveFromRoleAsync(userfound, role);
                            break;
                        }
                    }
                    if (await roleManager.RoleExistsAsync(request.UserRole) && request.ToAdd)
                    {
                        await userManager.AddToRoleAsync(userfound, request.UserRole);
                    }
                }

                _logger.LogInformation($"UpdateUserRoleCommand : Role {request.UserRole} add to user {userfound.UserName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
