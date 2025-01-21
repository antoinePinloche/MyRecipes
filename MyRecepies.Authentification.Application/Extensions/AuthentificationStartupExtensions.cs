using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Authentification.Application.Overrides;
using MyRecipes.Authentification.Repository.EF.Configuration;

namespace MyRecipes.Authentification.Application.Extensions
{
    public static class AuthentificationStartupExtensions
    {
        public static void AddAuthentificationEx(this IServiceCollection services, string? configuration)
        {
            services.AddServiceCollectionAuthentificationRepositoryEF(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AuthentificationStartupExtensions).Assembly));
        }

        public static async void AuthentificationDataBaseCreateOrUpdate(this WebApplication webApp)
        {
            await webApp.InitOrUpdateAuthentificationDbExtension();
        }

        public static void AddMapIdentityApi(this WebApplication webApplication)
        {
            webApplication.MapIdentityApiFilterable<Domain.Entities.User>(
                new IdentityApiEndpointsBuilderOptions()
                {
                    IncludeRegisterPost = true,
                    IncludeLoginPost = true,
                    IncludeRefreshPost = true,
                    IncludeConfirmEmailGet = false,
                    IncludeResendConfirmationEmailPost = false,
                    IncludeForgotPasswordPost = true,
                    IncludeResetPasswordPost = true,
                    // setting IncludeManageGroup to true will disable
                    // 2FA and both Info Actions
                    IncludeManageGroup = false,
                    Include2faPost = false,
                    IncludegInfoGet = false,
                    IncludeInfoPost = false
                }
                );
        }
    }
}
