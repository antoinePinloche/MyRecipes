using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Recipes.Domain.Repository.RepositoryFoodType;
using MyRecipes.Recipes.Domain.Repository.RepositoryIngredient;
using MyRecipes.Recipes.Domain.Repository.RepositoryInstruction;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipe;
using MyRecipes.Recipes.Domain.Repository.RepositoryRecipeIngredient;
using MyRecipes.Recipes.Repository.EF.DbContext;
using MyRecipes.Recipes.Repository.EF.Repository;

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

                services.AddTransient<IIngredientRepository, EFIngredientRepository>();
                services.AddTransient<IRecipeIngredientRepository, EFRecipeIngredientRepository>();
                services.AddTransient<IRecipesRepository, EFRecipeRepository>();
                services.AddTransient<IInstructionRepository, EFInstructionRepository>();
                services.AddTransient<IFoodTypeRepository, EFFoodTypeRepository>();
            }
            return services;
        }

        public static async Task InitOrUpdateRecipesDbExtension(this WebApplication webApp)
        {
            using var scope = webApp.Services.CreateScope();
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<RecipeDbContext>();

                bool pendingMogration = (await dbContext.Database.GetPendingMigrationsAsync()).Any();
                if (pendingMogration)
                {
                    await dbContext.Database.MigrateAsync();
                }
            }
        }
    }
}
