using MyRecipes.Authentification.Domain.Entities;
using MyRecipes.Transverse.Interface;

namespace MyRecipes.Authentification.Domain.Repository.RepositoryUser
{
    public interface IUsersRepository : IRepository<User, Guid>
    {
    }
}
