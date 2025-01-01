using MyRecipes.Authentification.Domain.Entities;

namespace MyRecipes.Authentification.Domain.Repository.RepositoryUser
{
    public interface IUsersRepository : IRepository<User, Guid>
    {
    }
}
