using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecepies.Authentification.Domain.Repository.RepositoryUser;
using MyRecepies.Authentification.Repository.EF.DbContext;
using System.Net;

namespace MyRecepies.Authentification.Repository.EF.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceCollectionAuthentificationRepositoryEF(this IServiceCollection services, string ConnectionString)
        {
            services.AddDbContext<AuthentificationDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString);
            });

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ServiceCollectionExtensions).Assembly));

            services.AddTransient<IUsersRepository, EFUserRepository>();

            return services;
        }
    }
}
