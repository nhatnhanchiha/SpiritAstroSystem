using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Planet;
using SpiritAstro.BusinessTier.ViewModels.Follow;
using SpiritAstro.BusinessTier.ViewModels.Planet;
using SpiritAstro.DataTier.Models;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class PlanetModule
    {
        public static void ConfigPlanetMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Planet, PlanetModel>();
            mc.CreateMap<CreatePlanetRequest, Planet>();
            mc.CreateMap<UpdatePlanetRequest, Planet>();
        }
    }
}