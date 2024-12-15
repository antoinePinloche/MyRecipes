using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecepies.Recepies.Application.Extensions
{
    public static class RecepiesStartupExtensions
    {
        public static void AddAuthentificationEx(this IServiceCollection services, string configuration)
        {
            //services.AddServiceCollectionAuthentificationRepositoryEF(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RecepiesStartupExtensions).Assembly));
        }
    }
}
