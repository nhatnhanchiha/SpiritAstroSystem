using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Token;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class TokenModule
    {
        public static void ConfigTokenMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateTokenRequest, Token>();
        }
    }
}