using Microsoft.EntityFrameworkCore;
using MyRecipes.Authentification.Domain.Entities;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Authentification.Repository.EF.DbContext;

namespace MyRecipes.Authentification.Repository.EF
{
    public class EFUserRepository : UsersBase
    {
        public AuthentificationDbContext Context { get; set; }

        public EFUserRepository(AuthentificationDbContext context) => Context = context;

        public async override Task<User> AddAsync(User entity)
        {
            var result = await Context.Users.AddAsync(entity);
            await Context.SaveChangesAsync();
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
            User? result = await Context.Users.FirstOrDefaultAsync(f => f.Id == key.ToString());
            if (result is null)
                return null;
            return result;
        }

        public override async Task RemoveAsync(User entitie)
        {
            User? userFound = await Context.Users.FirstOrDefaultAsync(f => f == entitie);
            if (userFound != null)
            {
                Context.Users.Remove(entitie);
                await Context.SaveChangesAsync();
            }
        }

        public override Task RemoveRangeAsync(ICollection<User> entities)
        {
            throw new NotImplementedException();
        }

        public async override Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public override async Task UpdateAsync(User entity)
        {
            Context.Users.Update(entity);
            await SaveAsync();
        }

        public override Task UpdateRangeAsync(ICollection<User> entities)
        {
            throw new NotImplementedException();
        }

        public override async Task CreateOrUpdateSchemaAsync()
        {
            bool pendingMigration = (await Context.Database.GetPendingMigrationsAsync()).Any();
            if (pendingMigration)
            {
                await Context.Database.MigrateAsync();
            }
        }
    }
}
