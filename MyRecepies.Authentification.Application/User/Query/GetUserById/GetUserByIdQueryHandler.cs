using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Transverse.Constant;
using MyRecipes.Transverse.Exception;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Authentification.Application.User.Query.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Domain.Entities.User>
    {
        private IUsersRepository _usersRepository;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        public GetUserByIdQueryHandler(IUsersRepository usersRepository, ILogger<GetUserByIdQueryHandler> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public async Task<Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Guid.IsEmpty())
                {
                    throw new WrongParameterException(
                        _logger,
                        nameof(Handle),
                        "GetUserByIdQueryHandler",
                        Constant.EXCEPTION.TITLE.INVALIDE_PARAMETER,
                        Constant.EXCEPTION.WRONG_PARAMETER_MESSAGE.ID);
                }
                var result = await _usersRepository.GetAsync(request.Guid);
                if (result == null)
                {
                    throw new UserNotFoundException(
                        _logger,
                        nameof(Handle),
                        "GetUserByIdQueryHandler",
                        Constant.EXCEPTION.TITLE.NOT_FOUND,
                        $"User with Id {request.Guid} not found");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetUserByIdQueryHandler : throw Exception {ex.Message}");
                throw;
            }
        }
    }
}
