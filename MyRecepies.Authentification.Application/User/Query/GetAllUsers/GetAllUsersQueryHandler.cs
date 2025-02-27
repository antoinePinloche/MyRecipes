using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Transverse.Extension;

namespace MyRecipes.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResult>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;
        public GetAllUsersQueryHandler(IUsersRepository usersRepository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public async Task<GetAllUsersQueryResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _usersRepository.GetAllAsync();
                _logger.LogInformation("GetAllUsersQueryHandler : Finish with success and return all user");
                return new GetAllUsersQueryResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
