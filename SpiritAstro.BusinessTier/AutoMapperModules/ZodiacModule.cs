using AutoMapper;
using SpiritAstro.BusinessTier.Requests.Zodiac;
using SpiritAstro.BusinessTier.ViewModels.Zodiac;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class ZodiacModule
    {
        public static void ConfigZodiacMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateZodiacRequest, Zodiac>();
            mc.CreateMap<Zodiac, ZodiacModel>().ReverseMap();
            mc.CreateMap<UpdateZodiacRequest, Zodiac>();
        }
    }
}
