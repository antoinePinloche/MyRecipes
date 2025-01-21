using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Recipes.Repository.EF.Configuration;

namespace MyRecipes.Recipes.Application.Extensions
{
    public static class RecipesStartupExtensions
    {
        public static void AddRecipesEx(this IServiceCollection services, string? configuration)
        {
            services.AddServiceCollectionRecipesRepositoryEF(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(RecipesStartupExtensions).Assembly));
        }

        public static async void RecipeDataBaseCreateOrUpdate(this WebApplication webApp)
        {
            await webApp.InitOrUpdateRecipesDbExtension();
        }
    }
}
