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
            var services = scope.ServiceProvider;

            var IngredientContext = services.GetRequiredService<IIngredientRepository>();
            var RecipeIngredientContext = services.GetRequiredService<IRecipeIngredientRepository>();
            var RecipesContext = services.GetRequiredService<IRecipesRepository>();
            var IInstructionContext = services.GetRequiredService<IInstructionRepository>();
            var IFoodTypeContext = services.GetRequiredService<IFoodTypeRepository>();

            EFIngredientRepository contextIngredient = (EFIngredientRepository)IngredientContext;
            EFRecipeIngredientRepository contextRecipeIngredient = (EFRecipeIngredientRepository)RecipeIngredientContext;
            EFRecipeRepository contextRecipe = (EFRecipeRepository)RecipesContext;
            EFInstructionRepository contextInstruction = (EFInstructionRepository)IInstructionContext;
            EFFoodTypeRepository contextFoodType = (EFFoodTypeRepository)IFoodTypeContext;

            await contextIngredient.CreateOrUpdateSchemaAsync();
            await contextRecipeIngredient.CreateOrUpdateSchemaAsync();
            await contextRecipe.CreateOrUpdateSchemaAsync();
            await contextInstruction.CreateOrUpdateSchemaAsync();
            await contextFoodType.CreateOrUpdateSchemaAsync();
        }
    }
}
