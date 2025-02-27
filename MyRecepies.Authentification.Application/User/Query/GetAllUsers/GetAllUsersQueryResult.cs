using MyRecipes.Transverse.Extension;

namespace MyRecipes.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryResult
    {
        public List<Domain.Entities.User> users {  get; set; }

        public GetAllUsersQueryResult(ICollection<Domain.Entities.User> user)
        {
            if (!user.IsNullOrEmpty())
                users = user.ToList();
            users = new List<Domain.Entities.User>();
        }
    }
}
