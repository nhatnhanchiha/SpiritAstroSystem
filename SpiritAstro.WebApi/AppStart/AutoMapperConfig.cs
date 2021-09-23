using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SpiritAstro.BusinessTier.AutoMapperModules;
using SpiritAstro.BusinessTier.Commons;

namespace SpiritAstro.WebApi.AppStart
{
    public static class AutoMapperConfig
    {
        public static void ConfigureAutoMapperServices(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperResolver());
                mc.ConfigCategoryMapperModule();
                mc.ConfigUserMapperModule();
                mc.ConfigFamousPersonMapperModule();
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
