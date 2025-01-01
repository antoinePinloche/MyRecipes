using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Recipes.Repository.EF.DbContext;

namespace MyRecipes.Recipes.Repository.EF.Configuration
{
    public static class ServiceCollectionRecipesRepositoryEF
    {
        public static IServiceCollection AddServiceCollectionRecipesRepositoryEF(this IServiceCollection services, string? ConnectionString)
        {
            if (ConnectionString is not null)
            {
                services.AddDbContext<RecipeDbContext>(options =>
                {
                    options.UseSqlServer(ConnectionString);
                });
            }
            return services;
        }
    }
}
