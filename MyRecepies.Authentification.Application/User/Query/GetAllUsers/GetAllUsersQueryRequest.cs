using MediatR;

namespace MyRecipes.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResult>
    {
        public GetAllUsersQueryRequest() { }
    }
}
