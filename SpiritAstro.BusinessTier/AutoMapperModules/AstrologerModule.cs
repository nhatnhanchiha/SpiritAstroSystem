using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Astrologer;
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
            mc.CreateMap<RegisterAstrologerRequest, Astrologer>()
                .ForMember(des => des.Id, opt 
                => opt.MapFrom(src => src.UserId));
            mc.CreateMap<UpdateAstrologerRequest, Astrologer>();
        }
    }
}