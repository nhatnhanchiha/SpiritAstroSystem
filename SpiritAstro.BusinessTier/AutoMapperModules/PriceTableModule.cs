using AutoMapper;
using SpiritAstro.BusinessTier.Requests.PriceTable;
using SpiritAstro.BusinessTier.ViewModels.PriceTable;
using SpiritAstro.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiritAstro.BusinessTier.AutoMapperModules
{
    public static class PriceTableModule
    {
        public static void ConfigPriceTableMapperModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<CreatePriceTableRequest, PriceTable>();
            mc.CreateMap<PriceTable, PriceTableModel>().ReverseMap();
            mc.CreateMap<UpdatePriceTableRequest, PriceTable>();
        }
    }
}
