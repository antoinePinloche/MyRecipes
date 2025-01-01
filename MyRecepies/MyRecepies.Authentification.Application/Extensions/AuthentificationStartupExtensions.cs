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

        public static void AddMapIdentityApi(this WebApplication webApplication)
        {
            webApplication.MapIdentityApi<Domain.Entities.User>();
            webApplication.MapIdentityApiFilterable<Domain.Entities.User>(
                new IdentityApiEndpointsBuilderOptions()
                {
                    IncludeRegisterPost = true,
                    IncludeLoginPost = true,
                    IncludeRefreshPost = true,
                    IncludeConfirmEmailGet = true,
                    IncludeResendConfirmationEmailPost = false,
                    IncludeForgotPasswordPost = false,
                    IncludeResetPasswordPost = false,
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
