using AutoMapper;
using SpiritAstro.BusinessTier.Commons.Enums.CustomerZodiac;
using SpiritAstro.BusinessTier.Requests.CustomerZodiac;
using SpiritAstro.BusinessTier.ViewModels.CustomerZodiac;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class CustomerZodiacModule
    {
        public static void ConfigCustomerZodiacMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CustomerZodiacRequest, CustomerZodiac>()
                .ForMember(des => des.Type, opt => opt.MapFrom(ignore => (int)CustomerZodiacType.Active));

            mc.CreateMap<CustomerZodiac, CustomerZodiacModel>().ReverseMap();
        }
    }
}
