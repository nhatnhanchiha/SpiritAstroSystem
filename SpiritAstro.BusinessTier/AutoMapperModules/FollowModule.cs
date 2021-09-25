using AutoMapper;
using SpiritAstro.BusinessTier.ViewModels.Follow;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class FollowModule
    {
        public static void ConfigFollowMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Follow, FollowWithAstrologer>()
                .ForMember(des => des.Astrologer, opt 
                    => opt.MapFrom(src => src.Astrologer));
            mc.CreateMap<Follow, FollowWithCustomer>()
                .ForMember(des => des.Customer, opt
                    => opt.MapFrom(src => src.Customer));
        }
    }
}