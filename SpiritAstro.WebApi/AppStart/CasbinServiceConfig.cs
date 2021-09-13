using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.AppStart
{
    public static class CasbinServiceConfig
    {
        public static void InitCasbin(this IServiceCollection services)
        {
            services.AddScoped<ICasbinService, CasbinService>();
        }
    }
}