using MediatR;
using MyRecepies.Authentification.Domain.Repository.RepositoryUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResult>
    {
        private readonly IUsersRepository _usersRepository;
        public GetAllUsersQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }


        public async Task<GetAllUsersQueryResult> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _usersRepository.GetAllAsync();
            return new GetAllUsersQueryResult(result);
        }
    }
}
