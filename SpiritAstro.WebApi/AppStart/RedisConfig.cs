using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.Services;

namespace SpiritAstro.WebApi.AppStart
{
    public static class RedisConfig
    {
        public static void ConfigureRedisServices(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = "localhost";
            });
            
            services.AddSingleton<IRedisService, RedisService>();
        }
    }
}