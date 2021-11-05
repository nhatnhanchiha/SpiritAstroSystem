using System.Linq;
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
            mc.CreateMap<Astrologer, AstrologerInFollow>();
            mc.CreateMap<Astrologer, PublicAstrologerModel>()
                .ForMember(des => des.FollowersCount, opt
                => opt.MapFrom(src => src.Follows.Count(f => f.AstrologerId == src.Id)));
            mc.CreateMap<RegisterAstrologerRequest, Astrologer>()
                .ForMember(des => des.Id, opt 
                => opt.MapFrom(src => src.UserId));
            mc.CreateMap<UpdateAstrologerRequest, Astrologer>();
            mc.CreateMap<Astrologer, PublicAstrologerModelForAdmin>();
        }
    }
}