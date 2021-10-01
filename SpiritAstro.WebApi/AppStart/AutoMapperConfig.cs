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
                mc.ConfigFamousPersonMapperModule();
                mc.ConfigFieldMapperModule();
                mc.ConfigCustomerZodiacMapperModule();
                mc.ConfigPostMapperModule();
                mc.ConfigBookingMapperModule();
                mc.ConfigAstrologerMapperModule();
                mc.ConfigFollowMapperModule();
                mc.ConfigCustomerMapperModule();
                mc.ConfigPostZodiacMapperModule();
                mc.ConfigPriceTableMapperModule();
                mc.ConfigPaymentMapperModule();
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
