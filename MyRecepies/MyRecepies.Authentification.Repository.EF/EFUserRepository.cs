using Microsoft.EntityFrameworkCore;
using MyRecepies.Authentification.Domain.Entities;
using MyRecepies.Authentification.Domain.Repository.RepositoryUser;
using MyRecepies.Authentification.Repository.EF.DbContext;

namespace MyRecepies.Authentification.Repository.EF
{
    public class EFUserRepository : UsersBase
    {
        public AuthentificationDbContext Context { get; set; }

        public EFUserRepository(AuthentificationDbContext context)
        {
            Context = context;
        }
        public async override Task<User> AddAsync(User entity)
        {
            var result = await Context.Users.AddAsync(entity);
            //await Context.SaveChangesAsync();
            await SaveAsync();
            return result.Entity;
        }

        public override Task<User> AddRangeAsync(ICollection<User> entities)
        {
            throw new NotImplementedException();
        }

        public override User FirstOrDefault(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public override async Task<ICollection<User>> GetAllAsync()
        {
            var result  = await Context.Users.ToListAsync();
            return result;
        }

        public override async Task<User> GetAsync(Guid key)
        {
            var result = await Context.Users.FirstOrDefaultAsync(f => f.Id == key);
            if (result is null)
                return null;
            return (User)result;
        }

        public override Task RemoveAsync(User entitie)
        {
            throw new NotImplementedException();
        }

        public override Task RemoveRangeAsync(ICollection<User> entities)
        {
            throw new NotImplementedException();
        }

        public async override Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public override Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateRangeAsync(ICollection<User> entities)
        {
            throw new NotImplementedException();
        }
    }
}
