using MediatR;

namespace MyRecipes.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<GetAllUsersQueryResult>
    {
        public GetAllUsersQuery() { }
    }
}
