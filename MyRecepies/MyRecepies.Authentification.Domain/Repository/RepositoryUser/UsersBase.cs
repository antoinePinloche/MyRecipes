using MyRecepies.Authentification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Domain.Repository.RepositoryUser
{
    public abstract class UsersBase : IUsersRepository
    {
        public abstract Task<User> AddAsync(User key);
        public abstract Task<User> AddRangeAsync(ICollection<User> entities);
        public abstract User FirstOrDefault(Func<User, bool> predicate);
        public abstract Task<ICollection<User>> GetAllAsync();
        public abstract Task<User> GetAsync(Guid key);
        public abstract Task RemoveAsync(User entitie);
        public abstract Task RemoveRangeAsync(ICollection<User> entities);
        public abstract Task SaveAsync();
        public abstract Task UpdateAsync(User entity);
        public abstract Task UpdateRangeAsync(ICollection<User> entities);

    }
}
