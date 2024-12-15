using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecepies.Authentification.Repository.EF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Authentification.Application.Extensions
{
    public static class AuthentificationStartupExtensions
    {
        public static void AddAuthentificationEx(this IServiceCollection services, string configuration)
        {
            services.AddServiceCollectionAuthentificationRepositoryEF(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AuthentificationStartupExtensions).Assembly));
        }
    }
}
