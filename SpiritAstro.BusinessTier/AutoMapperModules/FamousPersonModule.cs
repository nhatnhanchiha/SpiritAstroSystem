using AutoMapper;
using SpiritAstro.BusinessTier.Requests.FamousPerson;
using SpiritAstro.BusinessTier.ViewModels.FamousPerson;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class FamousPersonModule
    {
        public static void ConfigFamousPersonMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreateFamousPersonRequest, FamousPerson>();
            mc.CreateMap<FamousPerson, FamousPersonModel>().ReverseMap();
            mc.CreateMap<UpdateFamousPersonRequest, FamousPerson>();
        }
    }
}
