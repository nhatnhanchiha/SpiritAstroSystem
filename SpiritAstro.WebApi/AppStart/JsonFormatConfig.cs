using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SpiritAstro.WebApi.AppStart
{
    public static class JsonFormatConfig
    {
        public static void ConfigureJsonFormatServices(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
        }
    }
}
