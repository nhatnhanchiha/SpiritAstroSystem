using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Zodiac;
using SpiritAstro.BusinessTier.ViewModels.Users;
using SpiritAstro.BusinessTier.ViewModels.Zodiac;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class UserModule
    {
        public static void ConfigUserMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<User, UserModel>();
        }
    }
}