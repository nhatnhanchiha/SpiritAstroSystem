using AutoMapper;
using SpiritAstro.BusinessTier.ViewModels.Astrologer;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class AstrologerModule
    {
        public static void ConfigAstrologerMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Astrologer, AstrologerModel>();
            mc.CreateMap<Astrologer, PublicAstrologerModel>();
        }
    }
}