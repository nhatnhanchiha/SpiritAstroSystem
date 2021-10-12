using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.AppStart
{
    public static class AstroChartConfig
    {
        public static void ConfigureAstroChartServices(this IServiceCollection services)
        {
            services.AddScoped<IAstroChartService, AstroChartService>();
        }
    }
}