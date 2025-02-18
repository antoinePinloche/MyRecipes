using MyRecipes.Authentification.Domain.Entities;

namespace MyRecipes.Authentification.Domain.Repository.RepositoryUser
{
    public abstract class UsersBase : IUsersRepository
    {
        public abstract Task<User> AddAsync(User key);
        public abstract Task<User> AddRangeAsync(ICollection<User> entities);
        public abstract User FirstOrDefault(Func<User, bool> predicate);
        public abstract Task<ICollection<User>> GetAllAsync();
        public abstract Task<User> GetAsync(Guid key);
        public abstract Task RemoveRangeAsync(ICollection<User> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(User entity);
        public abstract Task UpdateRangeAsync(ICollection<User> entities);

        public abstract Task CreateOrUpdateSchemaAsync();
        public abstract Task<bool> ChangeRoleUserAsync(User user, string newRole);
        public abstract Task RemoveAsync(User entitie);
    }
}
