using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            throw new ArgumentNullException(nameof(ConnectionString));
        }
    
        public static async Task InitOrUpdateAuthentificationDbExtension(this WebApplication webApp)
        {
            using var scope = webApp.Services.CreateScope();
            var services = scope.ServiceProvider;

            var AuthentificationContext = services.GetRequiredService<IUsersRepository>();
            EFUserRepository context = (EFUserRepository)AuthentificationContext;

            await context.CreateOrUpdateSchemaAsync();
        }
    }
}
