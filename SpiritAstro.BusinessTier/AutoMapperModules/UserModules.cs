using AutoMapper;
using SpiritAstro.BusinessTier.Requests.User;
using SpiritAstro.BusinessTier.ViewModels.Users;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class UserModules
    {
        public static void ConfigUserMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<DetailUserRequest, User>();
            mc.CreateMap<User, UserModels>().ReverseMap();
        }
    }
}

