using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Authentification.Domain.Entities;
using MyRecipes.Authentification.Domain.Repository.RepositoryUser;
using MyRecipes.Authentification.Repository.EF.DbContext;

namespace MyRecipes.Authentification.Repository.EF.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceCollectionAuthentificationRepositoryEF(this IServiceCollection services, string? ConnectionString)
        {
            if (ConnectionString is not null)
            {
                services.AddDbContext<AuthentificationDbContext>(options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
                
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceCollectionExtensions).Assembly));
                services.AddTransient<IUsersRepository, EFUserRepository>();

                services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
                services.AddAuthorizationBuilder();
                services.AddIdentityCore<User>().AddEntityFrameworkStores<AuthentificationDbContext>().AddApiEndpoints();
                return services;
            }
            throw new NotImplementedException();
        }
    }
}
