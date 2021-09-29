using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.AppStart
{
    public static class CasbinConfig
    {
        public static void ConfigureCasbinServices(this IServiceCollection services)
        {
            services.AddScoped<ICasbinService, CasbinService>();
        }
    }
}