using MediatR;
using Microsoft.Extensions.Logging;
using MyRecipes.Authentification.Application.User.Command.DeleteUser;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Authentification.Application.User.Query.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Domain.Entities.User>
    {
        private IUsersRepository _usersRepository;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        private readonly IServiceProvider _serviceProvider;
        public GetUserByIdQueryHandler(IUsersRepository usersRepository, IServiceProvider serviceProvider, ILogger<GetUserByIdQueryHandler> logger)
        {
            _usersRepository = usersRepository;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<Domain.Entities.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _usersRepository.GetAsync(request.Guid);
        }
    }
}
