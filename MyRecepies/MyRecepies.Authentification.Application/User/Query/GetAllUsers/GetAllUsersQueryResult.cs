namespace MyRecipes.Authentification.Application.User.Query.GetAllUsers
{
    public class GetAllUsersQueryResult
    {
        public List<Domain.Entities.User> users {  get; set; }

        public GetAllUsersQueryResult(ICollection<Domain.Entities.User> user)
        {
            if (user is not null && user.Count() > 0)
                users = user.ToList();
            user = new List<Domain.Entities.User>();
        }
    }
}
