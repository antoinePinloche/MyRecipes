using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipes.Recipes.Application.Extensions
{
    public static class RecipesStartupExtensions
    {
        public static void AddAuthentificationEx(this IServiceCollection services, string configuration)
        {
            //services.AddServiceCollectionAuthentificationRepositoryEF(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RecipesStartupExtensions).Assembly));
        }
    }
}
