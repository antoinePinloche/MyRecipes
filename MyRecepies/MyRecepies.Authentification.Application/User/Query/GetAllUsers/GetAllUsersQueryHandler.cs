using MediatR;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;

namespace MyRecipes.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResult>
    {
        private readonly IUsersRepository _usersRepository;
        public GetAllUsersQueryHandler(IUsersRepository usersRepository) => _usersRepository = usersRepository;

        public async Task<GetAllUsersQueryResult> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _usersRepository.GetAllAsync();
            return new GetAllUsersQueryResult(result);
        }
    }
}
